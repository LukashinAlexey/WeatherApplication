using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WeatherBLL.Interfaces;
using WeatherBLL.Models;
using WeatherBLL.MyConfig;

namespace WeatherBLL.Services
{
    public class WeatherService : IWeatherService
    {
        public const int SecondsInOneDay = 86400;
        public const int SecondsInOneHour = 3600;
        public const int SecondsInOneMinute = 60;
        public const double ValueCelciusAtZeroKelvin = -273.15;
        private readonly IWeatherDataProvider _weatherDataProvider;


        public WeatherService(IWeatherDataProvider weatherDataProvider, IOptions<MyConf> key)
        {
            
            _weatherDataProvider = weatherDataProvider;
            MyConf = key.Value;
        }

        public MyConf MyConf { get; }

        public async Task<WeatherDataModel> GetWeatherDataByCityAsync(string city)
        {
            WeatherDataModel weatherData;
            try
            {
                weatherData = await _weatherDataProvider.GetWeatherDataAsync(city, MyConf.Key);

                if (weatherData == null)
                {
                    throw new ApplicationException("WeatherData is null");
                }

                SetIconURL(weatherData);
                ConvertKelvinToCelcil(weatherData);
                ConvertTimeToString(weatherData);
            }
            catch (Exception ex)
            {
                weatherData = null;
            }

            return weatherData;
        }

        private void ConvertTimeToString(WeatherDataModel weatherData)
        {
            if (weatherData.MainWeather == null)
            {
                throw new ApplicationException("Sys is null");
            }

            weatherData.Sys.Sunrise += weatherData.Timezone;
            weatherData.Sys.Sunset += weatherData.Timezone;

            weatherData.Sys.SunriseText = ConvertTime(weatherData.Sys.Sunrise);
            weatherData.Sys.SunsetText = ConvertTime(weatherData.Sys.Sunset);
        }

        private void ConvertKelvinToCelcil(WeatherDataModel weatherData)
        {
            if (weatherData.MainWeather == null)
            {
                throw new ApplicationException("MainWeather is null");
            }

            weatherData.MainWeather.MinimalTemperature += ValueCelciusAtZeroKelvin;
            weatherData.MainWeather.MaximalTemperature += ValueCelciusAtZeroKelvin;
            weatherData.MainWeather.Temperature += ValueCelciusAtZeroKelvin;
        }

        private void SetIconURL(WeatherDataModel weatherData)
        {
            if (weatherData.WeatherElement == null)
            {
                throw new ApplicationException("WeatherElement is null");
            }

            weatherData.WeatherElement.ForEach(x =>
            {
                x.Icon = $"http://openweathermap.org/img/wn/{x.Icon}@2x.png";
            });
        }

        private string ConvertTime(int time)
        {
            time %= SecondsInOneDay;
            int hours = time / SecondsInOneHour;
            int minutes = time / SecondsInOneMinute - hours * SecondsInOneMinute;
            int seconds = time - (hours * SecondsInOneHour) - (minutes * SecondsInOneMinute);

            return $"{hours}:{minutes}:{seconds}";
        }
    }
}
