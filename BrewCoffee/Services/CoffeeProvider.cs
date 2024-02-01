using BrewCoffee.Exceptions;
using BrewCoffee.Models;

namespace BrewCoffee.Services;

public interface ICoffeeProvider
{
    CoffeeItem MakeACoffee(string message, string when);
    int GetCoffeeCount();
}

public class CoffeeProvider : ICoffeeProvider
{
    public const int MaxCoffeeCount = 5;
    private int _coffeeCount = 0;

    public virtual CoffeeItem MakeACoffee(string message, string when)
    {
        _coffeeCount += 1;

        if (IsOutOfCoffee())
        {
            throw new OutOfCoffeeException();
        }

        return new CoffeeItem
        {
            Message = message,
            Prepared = when
        };
    }

    public virtual int GetCoffeeCount()
    {
        return _coffeeCount;
    }

    private bool IsOutOfCoffee()
    {
        return GetCoffeeCount() % MaxCoffeeCount == 0;
    }
}