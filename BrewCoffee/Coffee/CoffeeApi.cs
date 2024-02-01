using BrewCoffee.Exceptions;
using BrewCoffee.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrewCoffee.Coffee;

public static class CoffeeApi
{
    public static void MapCoffeeApi(this WebApplication app)
    {
        app.MapGet("/brew-coffee", MakeCoffee);
    }

    public static void AddCoffeeServices(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddSingleton<ICoffeeProvider, CoffeeProvider>();
        services.AddTransient<IBrewCoffeeService, BrewCoffeeService>();
    }

    public static IResult MakeCoffee([FromServices] IBrewCoffeeService brewCoffeeService)
    {
        try
        {
            var coffee = brewCoffeeService.MakeCoffee();
            return TypedResults.Ok(coffee);
        }
        catch (OutOfCoffeeException)
        {
            var errorStatusCode = StatusCodes.Status503ServiceUnavailable;
            var errorMessage = "Out of coffee!";
            return Results.Problem(errorMessage, statusCode: errorStatusCode);
        }
        catch (NoCoffeeDayException)
        {
            var errorStatusCode = StatusCodes.Status418ImATeapot;
            var errorMessage = "I'm a teapot!";
            return Results.Problem(errorMessage, statusCode: errorStatusCode);
        }
        catch (Exception)
        {
            var errorStatusCode = StatusCodes.Status500InternalServerError;
            var errorMessage = "Something went wrong! Contact support.";
            return Results.Problem(errorMessage, statusCode: errorStatusCode);
        }
    }
}