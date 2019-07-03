using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherBLL.Interfaces;
using WeatherBLL.Models;

namespace WeatherBLL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherDataProvider _weatherDataProvider;
        private const string _weaterAPIKey = "76dc8ff0fe330fb97e42bf67173c373b";


        public WeatherService(IWeatherDataProvider weatherDataProvider)
        {
            _weatherDataProvider = weatherDataProvider;
        }

        public async Task<WeatherDataModel> GetWeatherDataByCityAsync(string city)
        {
            WeatherDataModel weatherData;
            try
            {
                weatherData = await _weatherDataProvider.GetWeatherDataAsync(city, _weaterAPIKey);

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
            weatherData.Sys.Sunrise %= 86400;
            int hours = weatherData.Sys.Sunrise / 3600;
            int minutes = weatherData.Sys.Sunrise / 60 - hours * 60;
            int seconds = weatherData.Sys.Sunrise - (hours * 3600) - (minutes * 60);

            weatherData.Sys.SunriseText = hours.ToString() + " : " + minutes.ToString() + " : " + seconds.ToString();

            weatherData.Sys.Sunset += weatherData.Timezone;
            weatherData.Sys.Sunset %= 86400;
            int hours1 = weatherData.Sys.Sunset / 3600;
            int minutes1 = weatherData.Sys.Sunset / 60 - hours1 * 60;
            int seconds1 = weatherData.Sys.Sunset - hours1 * 3600 - minutes1 * 60;

            weatherData.Sys.SunsetText = hours1.ToString() + " : " + minutes1.ToString() + " : " + seconds1.ToString();


        }

        private void ConvertKelvinToCelcil(WeatherDataModel weatherData)
        {
            if (weatherData.MainWeather == null)
            {
                throw new ApplicationException("MainWeather is null");
            }
            weatherData.MainWeather.MinimalTemperature -= 273.15;

            weatherData.MainWeather.MaximalTemperature -= 273.15;

            weatherData.MainWeather.Temperature -= 273.15;
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


    }
}
