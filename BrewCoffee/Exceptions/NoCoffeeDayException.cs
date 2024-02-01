namespace BrewCoffee.Exceptions;

public class NoCoffeeDayException : Exception
{
    public NoCoffeeDayException() : base("No coffee today, it's a holiday!")
    {
    }
}