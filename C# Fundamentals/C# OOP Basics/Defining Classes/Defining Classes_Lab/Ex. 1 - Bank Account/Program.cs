using System;

public class Program
{
    public static void Main()
    {
        var acc = new BankAccount
        {
            Id = 1,
            Balance = 15
        };

        Console.WriteLine($"Account {acc.Id}, balance {acc.Balance}");
    }
}

