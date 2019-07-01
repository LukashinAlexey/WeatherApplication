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
            }
            catch (Exception ex)
            {
                weatherData = null;
            }

            return weatherData;
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
