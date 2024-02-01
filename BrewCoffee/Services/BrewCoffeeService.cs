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

    public BrewCoffeeService(TimeProvider timeProvider, ICoffeeProvider coffeeProvider)
    {
        _timeProvider = timeProvider;
        _coffeeProvider = coffeeProvider;
    }

    public CoffeeItem MakeCoffee()
    {
        if (IsNoCoffeeDay())
        {
            throw new NoCoffeeDayException();
        }

        return _coffeeProvider.MakeACoffee(
            "Your piping hot coffee is ready",
            _timeProvider.GetUtcNow().ToString("o", CultureInfo.InvariantCulture)
            );
    }


    private bool IsNoCoffeeDay()
    {
        return _timeProvider.GetUtcNow().Month == 4 && _timeProvider.GetUtcNow().Day == 1;
    }
}
