using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherBLL.Interfaces;
using WeatherBLL.Services;
using Moq;
using WeatherBLL.Models;
using System.Collections.Generic;
using WeatherBLL.MyConfig;
using Microsoft.Extensions.Options;

namespace UnitTests
{
    [TestClass]
    public class WeatherServiceTests
    {

        [TestMethod]
        public void GetWeatherDataByCityAsyncWithValidDataSuccessResult()
        {
            //Подготовка
            IOptions<MyConf> someOptions = Options.Create<MyConf>(new MyConf() { Key = "123" });           

            var weatherDataProvider = new Mock<IWeatherDataProvider>();
            var successResult = new WeatherDataModel()
            {
                Name = "successResult",
                Wind = new Wind()
                {
                    Speed = 2.5
                },
                MainWeather = new MainWeather()
                {
                    MaximalTemperature = 1.0,
                    MinimalTemperature = 1.0,
                    Temperature = 1.0,
                    Humidity = 20,
                    Pressure = 10.5
                },
                WeatherElement = new List<WeatherElement>() {
                    new WeatherElement()
                    {
                        Icon = "12d"
                    }
                },
                Sys = new Sys()
                {
                    Sunrise = 1562212165,
                    Sunset = 1562271597,
                    Country = "UA"
                },
                Timezone = 3600,                
            };            
            weatherDataProvider
                .Setup(x => x.GetWeatherDataAsync("Kharkiv", "123"))
                .ReturnsAsync(successResult);

            var service = new WeatherService(weatherDataProvider.Object, someOptions);

            //Выполнение
            var result = service.GetWeatherDataByCityAsync("Kharkiv").Result;

            //Проверка
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "successResult");
        }

        [TestMethod]
        public void GetWeatherDataByCityAsyncWithInvalidTempError()
        {
            //Подготовка
            IOptions<MyConf> someOptions = Options.Create<MyConf>(new MyConf() { Key = "123" });

            var weatherDataProvider = new Mock<IWeatherDataProvider>();
            var successResult = new WeatherDataModel()
            {
                Name = "successResult",
                Wind = new Wind()
                {
                    Speed = 2.5
                },
                MainWeather = null,
                WeatherElement = new List<WeatherElement>() {
                    new WeatherElement()
                    {
                        Icon = "12d"
                    }
                },
                Sys = new Sys()
                {
                    Sunrise = 1562212165,
                    Sunset = 1562271597,
                    Country = "UA"
                },
                Timezone = 3600,
            };
            weatherDataProvider
                .Setup(x => x.GetWeatherDataAsync("Kharkiv", "123"))
                .ReturnsAsync(successResult);

            var service = new WeatherService(weatherDataProvider.Object, someOptions);

            //Выполнение
            var result = service.GetWeatherDataByCityAsync("Kharkiv").Result;

            //Проверка
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetWeatherDataByCityAsyncInvalidIconError()
        {
            //Подготовка
            IOptions<MyConf> someOptions = Options.Create<MyConf>(new MyConf() { Key = "123" });

            var weatherDataProvider = new Mock<IWeatherDataProvider>();
            var successResult = new WeatherDataModel()
            {
                Name = "successResult",
                Wind = new Wind()
                {
                    Speed = 2.5
                },
                MainWeather = new MainWeather()
                {
                    MaximalTemperature = 1.0,
                    MinimalTemperature = 1.0,
                    Temperature = 1.0,
                    Humidity = 20,
                    Pressure = 10.5
                },
                WeatherElement = null,
                Sys = new Sys()
                {
                    Sunrise = 1562212165,
                    Sunset = 1562271597,
                    Country = "UA"
                },
                Timezone = 3600,
            };
            weatherDataProvider
                .Setup(x => x.GetWeatherDataAsync("Kharkiv", "123"))
                .ReturnsAsync(successResult);

            var service = new WeatherService(weatherDataProvider.Object, someOptions);

            //Выполнение
            var result = service.GetWeatherDataByCityAsync("Kharkiv").Result;

            //Проверка
            Assert.IsNull(result);
        }  
    }
}
