namespace BrewCoffee.Models;

public record CoffeeItem
{
    public string Message { get; set; } = default!;
    public string Prepared { get; set; } = default!;
}