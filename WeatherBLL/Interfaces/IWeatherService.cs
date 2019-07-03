using System.Threading.Tasks;
using WeatherBLL.Models;

namespace WeatherBLL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherDataModel> GetWeatherDataByCityAsync(string city);
    }
}
