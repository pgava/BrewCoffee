using BrewCoffee.Exceptions;
using BrewCoffee.Models;
using BrewCoffee.Services;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;

namespace BrewCoffee.Tests;

public class BrewCoffeeServiceTests
{
    [Fact]
    public void Should_Be_Able_To_Make_A_Coffee()
    {
        var fakeTimeProvider = new FakeTimeProvider();
        fakeTimeProvider.SetUtcNow(new DateTime(2024, 1, 1));

        var sut = new BrewCoffeeService(fakeTimeProvider, new CoffeeProvider());

        var result = sut.MakeCoffee();

        result.Should().BeEquivalentTo(new CoffeeItem
        {
            Message = "Your piping hot coffee is ready",
            Prepared = "2024-01-01T00:00:00.0000000+11:00"
        });
    }

    [Fact]
    public void Should_Return_Out_Of_Coffee_Error_When_Out_Of_Coffee()
    {
        var fakeTimeProvider = new FakeTimeProvider();
        fakeTimeProvider.SetUtcNow(new DateTime(2024, 1, 1));

        var sut = new BrewCoffeeService(fakeTimeProvider, new FakeCoffeeProvider());

        Action act = () => sut.MakeCoffee();

        act.Should().Throw<OutOfCoffeeException>();
    }

    [Fact]
    public void Should_Return_No_Coffee_Day_Error_When_No_Coffee_Day()
    {
        var fakeTimeProvider = new FakeTimeProvider();
        fakeTimeProvider.SetUtcNow(new DateTime(2024, 4, 1));

        var sut = new BrewCoffeeService(fakeTimeProvider, new CoffeeProvider());

        Action act = () => sut.MakeCoffee();

        act.Should().Throw<NoCoffeeDayException>();
    }
}