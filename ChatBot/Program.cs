// See https://aka.ms/new-console-template for more information
using static ChatBot.MyClass;
using System.Text.Json;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Microsoft.VisualBasic;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

//Console.WriteLine("Hello, World!");

StringBuilder sb;

using (HttpClient client = new HttpClient())
{
    using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
    {
        httpRequestMessage.Method = HttpMethod.Get;
        httpRequestMessage.RequestUri = new Uri("https://api.openweathermap.org/data/2.5/forecast?lat=52.03333&lon=8.53333&units=metric&appid=1405ae0f1cae8233da346faaf7af9466");
        HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);
        string content = await responseMessage.Content.ReadAsStringAsync();

        MyWeather? myWeather = JsonSerializer.Deserialize<MyWeather>(content);
        //var weatherDateTime = myWeather.List.Select(t => new { Time = t.DtTxt }).ToList();
        //var weatherTemp = myWeather.List.Select(t => new { Temp = t.Main.Temp }).ToList();
        var weatherLocal = myWeather!.List!.Select(t => new { Info = $"Data: {t.DtTxt} = Grad,C: {t.Main.Temp}" }).ToList();
        sb = new StringBuilder();
        foreach (var weather in weatherLocal)
        { 
            sb.AppendLine(weather.ToString());
        }

        //Console.WriteLine(sb.ToString());
    }
}

string token = "5560737665:AAFQ2O4UNCXufW8eG65MUM4IFRnUvPxJABQ";
var botClient = new TelegramBotClient(token);
var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    //if (message.Text is not { } messageText)
    //    return;
    var messageText = "";
    if (update.Message?.Type != null)
    {
        messageText = update.Message.Text;
    }

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    // Echo received message text
    if (messageText == "/weather")
    {
        Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: $"{sb}",
        cancellationToken: cancellationToken);
    }

    if (message.Type == MessageType.Location)
    {
        var location = message.Location;
        await botClient.SendTextMessageAsync(
            chatId,
            $"Latitude: {location?.Latitude}, Longitude: {location?.Longitude}",
            replyToMessageId: update!.Message!.MessageId,
            disableNotification: true,
            cancellationToken: cancellationToken
            );

        StringBuilder sbL;
        using (HttpClient client = new HttpClient())
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/forecast?lat={location?.Latitude}&lon={location?.Longitude}&units=metric&appid=1405ae0f1cae8233da346faaf7af9466");
                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMessage);
                string content = await responseMessage.Content.ReadAsStringAsync();

                MyWeather? myWeather = JsonSerializer.Deserialize<MyWeather>(content);
                //var weatherDateTime = myWeather.List.Select(t => new { Time = t.DtTxt }).ToList();
                //var weatherTemp = myWeather.List.Select(t => new { Temp = t.Main.Temp }).ToList();
                var weatherLocal = myWeather!.List!.Select(t => new { Info = $"Date: {t.DtTxt} = Grad: {t.Main.Temp} C" }).ToList();
                sbL = new StringBuilder();
                foreach (var weather in weatherLocal)
                {
                    sbL.AppendLine(weather.ToString());
                }
            }
        }

        Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: $"{sbL}",
        cancellationToken: cancellationToken);
    }

    if (messageText == "/help")
    {
        Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: $"Надішліть вашу локацію/Submit your location...",
        cancellationToken: cancellationToken);
    }
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}