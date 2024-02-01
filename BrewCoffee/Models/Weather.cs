namespace BrewCoffee.Models;

public class Weather
{
    public Main Main { get; set; } = new();
}

public class Main
{
    public decimal Temp { get; set; } = 0;
}