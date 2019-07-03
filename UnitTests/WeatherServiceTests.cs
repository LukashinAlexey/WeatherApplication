using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherBLL.Interfaces;
using WeatherBLL.Services;
using Moq;
using WeatherBLL.Models;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class WeatherServiceTests
    {

        [TestMethod]
        public void GetWeatherDataByCityAsyncWithValidDataSuccessResult()
        {
            //����������
            var weatherDataProvider = new Mock<IWeatherDataProvider>();
            var successResult = new WeatherDataModel()
            {
                Name = "successResult",
                MainWeather = new MainWeather()
                {
                    MaximalTemperature = 1.0,
                    MinimalTemperature = 1.0,
                    Temperature = 1.0
                },
                WeatherElement = new List<WeatherElement>(){
                    new WeatherElement()
                    {
                        Icon = "12d"
                    }
                }
            };
            weatherDataProvider.Setup(x => x.GetWeatherDataAsync("Kharkiv", It.IsAny<string>()))
                .ReturnsAsync(successResult);

            var service = new WeatherService(weatherDataProvider.Object);

            //����������
            var result = service.GetWeatherDataByCityAsync("Kharkiv").Result;

            //��������
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "successResult");
        }

        [TestMethod]
        public void GetWeatherDataByCityAsyncWithInvalidTempError()
        {
            //����������
            var weatherDataProvider = new Mock<IWeatherDataProvider>();
            var successResult = new WeatherDataModel()
            {
                Name = "successResult",
                MainWeather = null,
                WeatherElement = new List<WeatherElement>(){
                    new WeatherElement()
                    {
                        Icon = "12d"
                    }
                }
            };
            weatherDataProvider.Setup(x => x.GetWeatherDataAsync("InvalidTemp", It.IsAny<string>()))
                .ReturnsAsync(successResult);

            var service = new WeatherService(weatherDataProvider.Object);

            //����������
            var result = service.GetWeatherDataByCityAsync("InvalidTemp").Result;

            //��������
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetWeatherDataByCityAsyncInvalidIconError()
        {
            //����������
            var weatherDataProvider = new Mock<IWeatherDataProvider>();
            var successResult = new WeatherDataModel()
            {
                Name = "successResult",
                MainWeather = new MainWeather()
                {
                    MaximalTemperature = 1.0,
                    MinimalTemperature = 1.0,
                    Temperature = 1.0
                },
                WeatherElement = null
            };
            weatherDataProvider.Setup(x => x.GetWeatherDataAsync("InvalidIcon", It.IsAny<string>()))
                .ReturnsAsync(successResult);

            var service = new WeatherService(weatherDataProvider.Object);

            //����������
            var result = service.GetWeatherDataByCityAsync("InvalidIcon").Result;

            //��������
            Assert.IsNull(result);
        }  
    }
}
