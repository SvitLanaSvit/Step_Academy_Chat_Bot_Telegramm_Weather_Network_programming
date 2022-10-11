using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatBot
{
    public class MyClass
    {
        public class Main
        {

            [JsonPropertyName("temp")]
            public double Temp { get; set; }

            [JsonPropertyName("feels_like")]
            public double FeelsLike { get; set; }

            [JsonPropertyName("temp_min")]
            public double TempMin { get; set; }

            [JsonPropertyName("temp_max")]
            public double TempMax { get; set; }

            [JsonPropertyName("pressure")]
            public int Pressure { get; set; }

            [JsonPropertyName("sea_level")]
            public int SeaLevel { get; set; }

            [JsonPropertyName("grnd_level")]
            public int GrndLevel { get; set; }

            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }

            [JsonPropertyName("temp_kf")]
            public double TempKf { get; set; }
        }

        public class Weather
        {

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("main")]
            public string Main { get; set; } = null!;

            [JsonPropertyName("description")]
            public string Description { get; set; } = null!;

            [JsonPropertyName("icon")]
            public string Icon { get; set; } = null!;
        }

        public class Clouds
        {

            [JsonPropertyName("all")]
            public int All { get; set; }
        }

        public class Wind
        {

            [JsonPropertyName("speed")]
            public double Speed { get; set; }

            [JsonPropertyName("deg")]
            public int Deg { get; set; }

            [JsonPropertyName("gust")]
            public double Gust { get; set; }
        }

        public class Sys
        {

            [JsonPropertyName("pod")]
            public string Pod { get; set; } = null!;
        }

        public class Rain
        {

            [JsonPropertyName("3h")]
            public double RainR { get; set; }
        }

        public class List
        {

            [JsonPropertyName("dt")]
            public int Dt { get; set; }

            [JsonPropertyName("main")]
            public Main Main { get; set; } = null!;

            [JsonPropertyName("weather")]
            public IList<Weather> Weather { get; set; } = null!;

            [JsonPropertyName("clouds")]
            public Clouds Clouds { get; set; } = null!;

            [JsonPropertyName("wind")]
            public Wind Wind { get; set; } = null!;

            [JsonPropertyName("visibility")]
            public int Visibility { get; set; }

            [JsonPropertyName("pop")]
            public double Pop { get; set; }

            [JsonPropertyName("sys")]
            public Sys Sys { get; set; } = null!;

            [JsonPropertyName("dt_txt")]
            public string DtTxt { get; set; } = null!;

            [JsonPropertyName("rain")]
            public Rain Rain { get; set; } = null!;
        }

        public class Coord
        {

            [JsonPropertyName("lat")]
            public double Lat { get; set; }

            [JsonPropertyName("lon")]
            public double Lon { get; set; }
        }

        public class City
        {

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; } = null!;

            [JsonPropertyName("coord")]
            public Coord Coord { get; set; } = null!;

            [JsonPropertyName("country")]
            public string Country { get; set; } = null!;

            [JsonPropertyName("population")]
            public int Population { get; set; }

            [JsonPropertyName("timezone")]
            public int Timezone { get; set; }

            [JsonPropertyName("sunrise")]
            public int Sunrise { get; set; }

            [JsonPropertyName("sunset")]
            public int Sunset { get; set; }
        }

        public class MyWeather
        {

            [JsonPropertyName("cod")]
            public string Cod { get; set; } = null!;

            [JsonPropertyName("message")]
            public int Message { get; set; }

            [JsonPropertyName("cnt")]
            public int Cnt { get; set; }

            [JsonPropertyName("list")]
            public IList<List>? List { get; set; }

            [JsonPropertyName("city")]
            public City City { get; set; } = null!;
        }
    }
}
