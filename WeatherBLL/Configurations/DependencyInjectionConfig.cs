using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using WeatherBLL.Interfaces;
using WeatherBLL.Services;


namespace WeatherBLL.Configurations
{
    public static class DependencyInjectionConfig
    {
        private const string _weaterAPIURL = "http://api.openweathermap.org";//PascalCase

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();

            services.AddRefitClient<IWeatherDataProvider>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(_weaterAPIURL));
        }
    }
}
