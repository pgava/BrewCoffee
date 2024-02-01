using BrewCoffee.Services;

namespace BrewCoffee.Tests;

internal class FakeCoffeeProvider : CoffeeProvider
{
    public override int GetCoffeeCount()
    {
        return MaxCoffeeCount;
    }
}