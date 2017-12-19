using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using P01_BillsPaymentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using P01_BillsPaymentSystem.Data.Models;
using Type = P01_BillsPaymentSystem.Data.Models.Type;


namespace P01_BIllsPaymentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new BillsPaymentSystemContext();

            // CreateDb(ref context);
            // MigrateDb(ref context);
            // Seeder.SeedDb(ref context);
            // PrintInfo(ref context);

            
        }


        private static void MigrateDb(ref BillsPaymentSystemContext context)
        {
            using (context)
            {
                context.Database.Migrate();
            }
        }

        private static void CreateDb(ref BillsPaymentSystemContext context)
        {
            using (context)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        private static void PrintInfo(ref BillsPaymentSystemContext context)
        {
            Console.Write("Enter userId: ");

            var userId = int.Parse(Console.ReadLine());

            using (context)
            {
                var user = context.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        Name = $"{u.FirstName} " + $"{u.LastName}",
                        BankAccounts = u.PaymentMethods
                            .Where(pm => pm.Type == Type.BankAccount)
                            .Select(pm => pm.BankAccount).ToArray(),
                        CreditCards = u.PaymentMethods
                            .Where(pm => pm.Type == Type.CreditCard)
                            .Select(pm => pm.CreditCard).ToArray()
                    })
                    .FirstOrDefault();

                Console.WriteLine($"User: {user.Name}");

                if (user.BankAccounts.Any())
                {
                    Console.WriteLine("Bank Accounts:");
                    foreach (var ba in user.BankAccounts)
                    {
                        Console.WriteLine($"-- ID: {ba.BankAccountId}");
                        Console.WriteLine($"--- Balance: {ba.Balance}");
                        Console.WriteLine($"--- Bank: {ba.BankName}");
                        Console.WriteLine($"--- SWIFT: {ba.SwiftCode}");
                    }
                }

                if (user.CreditCards.Any())
                {
                    Console.WriteLine("Credit Cards:");
                    foreach (var cc in user.CreditCards)
                    {
                        Console.WriteLine($"-- ID: {cc.CreditCardId}");
                        Console.WriteLine($"--- Limit: {cc.Limit}");
                        Console.WriteLine($"--- Money Owed: {cc.MoneyOwed}");
                        Console.WriteLine($"--- Limit Left: {cc.LimitLeft}");
                        Console.WriteLine($"--- Expiration Date: {cc.ExpirationDate}");
                    }
                }
            }
        }
    }
}
