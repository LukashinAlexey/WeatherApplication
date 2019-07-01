using Refit;
using System.Threading.Tasks;
using WeatherBLL.Models;

namespace WeatherBLL.Interfaces
{
    public interface IWeatherDataProvider
    {
        [Get("/data/2.5/weather?q={city}&APPID={key}")]
        Task<WeatherDataModel> GetWeatherDataAsync(string city, string key);
    }
}
