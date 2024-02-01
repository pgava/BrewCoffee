using System.Globalization;
using BrewCoffee.Exceptions;
using BrewCoffee.Models;

namespace BrewCoffee.Services;

public interface IBrewCoffeeService
{
    CoffeeItem MakeCoffee();
}

public class BrewCoffeeService : IBrewCoffeeService
{
    private readonly TimeProvider _timeProvider;
    private readonly ICoffeeProvider _coffeeProvider;
    private readonly IWeatherProvider _weatherProvider;
    public const string HotCoffeeIsReady = "Your piping hot coffee is ready";
    public const string IcedCoffeeIsReady = "Your refreshing iced coffee is ready";

    public BrewCoffeeService(
        TimeProvider timeProvider,
        ICoffeeProvider coffeeProvider,
        IWeatherProvider weatherProvider)
    {
        _timeProvider = timeProvider;
        _coffeeProvider = coffeeProvider;
        _weatherProvider = weatherProvider;
    }

    public CoffeeItem MakeCoffee()
    {
        if (IsNoCoffeeDay())
        {
            throw new NoCoffeeDayException();
        }

        var weather = _weatherProvider.GetWeather().GetAwaiter().GetResult();

        var message = HotCoffeeIsReady;
        if (IsHotDay(weather))
        {
            message = IcedCoffeeIsReady;
        }

        return _coffeeProvider.MakeACoffee(
            message,
            GetIsoDate()
            );
    }

    private bool IsNoCoffeeDay()
    {
        return _timeProvider.GetUtcNow().Month == 4 && _timeProvider.GetUtcNow().Day == 1;
    }

    private bool IsHotDay(Weather? weather)
    {
        return weather is not null && weather.Main.Temp > 30;
    }

    private string GetIsoDate()
    {
        return _timeProvider.GetUtcNow().ToString("o", CultureInfo.InvariantCulture);
    }
}
