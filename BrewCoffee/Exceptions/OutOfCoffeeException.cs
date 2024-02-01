namespace BrewCoffee.Exceptions;

public class OutOfCoffeeException : Exception
{
    public OutOfCoffeeException() : base("Out of coffee")
    {
    }
}