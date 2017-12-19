using System;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        private decimal _balance;
        
        public int BankAccountId { get; set; }

        public decimal Balance
        {
            get => this._balance;
            private set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("Value cannot be zero or negative!");    
                }

                this._balance = value;
            }
        }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount > _balance)
            {
                throw new InvalidOperationException("Incufficient funds!");
            }

            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }
    }
}