

Console.Write("Enter a number: ");
var enteredNumber = Console.ReadLine();

var tom = DateTimeOffset.UtcNow.Tomorrow();

if(int.TryParse(enteredNumber, out int x))
{
   
    if (x.IsEven())
    {
        Console.Write("That is an even number!");
    }
    else
    {
        Console.WriteLine("That is an odd number");
    }
}
else
{
    Console.WriteLine("I Said a NUMBER, Moron.");
}


public static class Extensions
{
    public static bool IsEven(this int x)
    {
        return x % 2 == 0;
    }

    public static DateTimeOffset Tomorrow(this DateTimeOffset x)
    {
        return x.AddDays(1);
    }
}