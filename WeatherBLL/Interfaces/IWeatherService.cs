using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherBLL.Models;

namespace WeatherBLL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherDataModel> GetWeatherDataByCityAsync(string city);
    }
}
