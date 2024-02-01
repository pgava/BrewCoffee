using BrewCoffee.Coffee;
using BrewCoffee.Models;
using BrewCoffee.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Time.Testing;
namespace BrewCoffee.Tests;

public class CoffeeApiTests
{
    [Fact]
    public void When_Calling_The_Api_Should_Be_Able_To_Make_A_Coffee()
    {
        var fakeTimeProvider = new FakeTimeProvider();
        fakeTimeProvider.SetUtcNow(new DateTime(2024, 1, 1));

        var service = new BrewCoffeeService(fakeTimeProvider, new CoffeeProvider(), new FakeWeatherProvider());

        var result = CoffeeApi.MakeCoffee(service);
        var resultData = (result as Ok<CoffeeItem>)?.Value;
        resultData.Should().NotBeNull();
        resultData.Message.Should().Be("Your piping hot coffee is ready");
        resultData.Prepared.Should().Be("2024-01-01T00:00:00.0000000+11:00");
    }
}

