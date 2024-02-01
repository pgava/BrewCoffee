using BrewCoffee.Models;
using BrewCoffee.Services;

namespace BrewCoffee.Tests;

internal class FakeWeatherProvider : IWeatherProvider
{
    private readonly decimal _temp = 0;

    public FakeWeatherProvider()
    {

    }

    public FakeWeatherProvider(decimal temp)
    {
        _temp = temp;
    }
    public Task<Weather> GetWeather()
    {
        var weather = new Weather
        {
            Main = new Main
            {
                Temp = _temp
            }
        };

        return Task.FromResult(weather);
    }
}