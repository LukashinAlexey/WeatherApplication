using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherBLL.Models
{
    public class WeatherDataModel
    {
        [JsonProperty("coord")]
        public Coordinate Coordinate { get; set; }

        [JsonProperty("weather")]
        public List<WeatherElement> WeatherElement { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public MainWeather MainWeather { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt")]
        public int Dt { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("message")]
        public int Message { get; set; }
    }


    public class Coordinate
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class MainWeather
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("temp_min")]
        public double MinimalTemperature { get; set; }

        [JsonProperty("temp_max")]
        public double MaximalTemperature { get; set; }

        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public double GrndLevel { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Degree { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public string CloudsAll { get; set; }
    }

    public class Sys
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunrisetext")]
        public string SunriseText { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        [JsonProperty("sunsettext")]
        public string SunsetText { get; set; }
    }

    public class WeatherElement
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

    }
}