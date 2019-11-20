using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApplication.Models;
using WeatherBLL.Interfaces;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;

        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetWeatherDataByCity(string city)
        {
            var weather = await _weatherService.GetWeatherDataByCityAsync(city);
            return View(weather);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
