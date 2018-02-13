using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var accounts = new Dictionary<int, BankAccount>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var commands = input.Split();
            var commandType = commands[0];

            switch (commandType)
            {
                case "Create":
                    Create(commands, accounts);
                    break;
                case "Deposit":
                    Deposit(commands, accounts);
                    break;
                case "Withdraw":
                    Withdraw(commands, accounts);
                    break;
                case "Print":
                    Print(commands, accounts);
                    break;
            }
        }
    }

    private static void Create(string[] commands, Dictionary<int, BankAccount> accounts)
    {
        var accountId = int.Parse(commands[1]);
        if (AccountExists(accountId, accounts))
        {
            Console.WriteLine("Account already exists");
        }
        else
        {
            var newAccount = new BankAccount
            {
                Id = accountId
            };

            accounts.Add(accountId, newAccount);
        }
    }

    private static void Deposit(string[] commands, Dictionary<int, BankAccount> accounts)
    {
        var accountId = int.Parse(commands[1]);
        if (!AccountExists(accountId, accounts))
        {
            Console.WriteLine("Account does not exist");
        }
        else
        {
            var depositAmount = decimal.Parse(commands[2]);
            accounts[accountId].Deposit(depositAmount);
        }
    }

    private static void Withdraw(string[] commands, Dictionary<int, BankAccount> accounts)
    {
        var accountId = int.Parse(commands[1]);
        if (!AccountExists(accountId, accounts))
        {
            Console.WriteLine("Account does not exist");
        }
        else
        {
            try
            {
                var withdrawAmount = decimal.Parse(commands[2]);
                accounts[accountId].Withdraw(withdrawAmount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private static void Print(string[] commands, Dictionary<int, BankAccount> accounts)
    {
        var accountId = int.Parse(commands[1]);

        Console.WriteLine(
            !AccountExists(accountId, accounts) ? "Account does not exist"
                : accounts[accountId].ToString());
    }

    private static bool AccountExists(int accountId, Dictionary<int, BankAccount> accounts)
    {
        return accounts.ContainsKey(accountId);
    }
}