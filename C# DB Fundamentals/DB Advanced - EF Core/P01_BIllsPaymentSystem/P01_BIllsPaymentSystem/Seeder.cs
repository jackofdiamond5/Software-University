using System;
using System.Collections.Generic;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using Type = P01_BillsPaymentSystem.Data.Models.Type;

namespace P01_BIllsPaymentSystem
{
    public class Seeder
    {
        public static void SeedDb(ref BillsPaymentSystemContext context)
        {
            var users = GetUsers();
            var creditCards = GetCreditCards(users.Count);
            var bankAccounts = GetBankAccounts(users.Count);

            SeedPaymentMethods(ref context, users.Count, users, bankAccounts);

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static List<User> GetUsers()
        {
            var userFirstNames = new[] { "Pesho", "Gosho", "Stanio", "Ico", "Ivan", "Dragan", "Petkan" };
            var userLastNames = new[] { "Ivanov", "Todorov", "Peshev", "Goshec", "Penchev", "Bonchev", "Chochev" };

            var random = new Random();
            var users = new List<User>();

            for (int i = 0; i < userFirstNames.Length && i < userLastNames.Length; i++)
            {
                var userFirstName = userFirstNames[random.Next(0, userFirstNames.Length - 1)];
                var userLastName = userLastNames[random.Next(0, userFirstNames.Length - 1)];
                var email = userFirstName + "_" + userLastName + (i % 2 == 0 ? "@gmail.com" : "@hotmail.com");
                var password = userFirstName + "." + userLastName + i / 2;

                users.Add(new User
                {
                    FirstName = userFirstName,
                    LastName = userLastName,
                    Email = email,
                    Password = password
                });
            }

            return users;
        }

        private static IEnumerable<CreditCard> GetCreditCards(int usersCount)
        {
            var creditCards = new List<CreditCard>();

            for (int i = 0; i < usersCount; i++)
            {
                var expDate = DateTime.Now.AddYears(i + 1).AddDays(i + 1 * 2).AddMonths((i + 1 * 3) / 2);
                var limit = i * DateTime.Now.Year;
                var moneyOwned = DateTime.Now.Year - i * i;

                creditCards.Add(new CreditCard
                {
                    ExpirationDate = expDate,
                    Limit = limit,
                    MoneyOwed = moneyOwned
                });
            }

            return creditCards;
        }

        private static List<BankAccount> GetBankAccounts(int usersCount)
        {
            var bankAccounts = new List<BankAccount>();
            for (int i = 0; i < usersCount; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = (decimal)(i * Math.PI),
                    BankName = "Peshobank",
                    SwiftCode = "143141351DAWE14131" + DateTime.Now.Year * i * Math.PI
                };

                bankAccounts.Add(bankAccount);
            }

            return bankAccounts;
        }

        private static void SeedPaymentMethods(ref BillsPaymentSystemContext context, int usersCount, IReadOnlyList<User> users,
            IReadOnlyList<BankAccount> bankAccounts)
        {
            var paymentMethods = new List<PaymentMethod>();

            for (int i = 0; i < usersCount; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    User = users[i],
                    BankAccount = bankAccounts[i],
                    Type = Type.CreditCard
                };

                paymentMethods.Add(paymentMethod);
            }

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }
    }
}
