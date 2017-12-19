using System;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        private decimal _moneyOwed;
        private decimal _limit;

        public int CreditCardId { get; set; }

        public decimal Limit
        {
            get => _limit;
            private set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("Value cannot be zero or negative!");
                }

                this._limit = value;
            }
        }

        public decimal MoneyOwed
        {
            get => _moneyOwed;
            private set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("Value cannot be zero or negative!");
                }

                this._moneyOwed = value;
            }
        }

        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (this.LimitLeft < amount)
            {
                throw new InvalidOperationException("Incufficient funds!");
            }

            this.MoneyOwed += amount;
            this.Limit -= amount;
        }

        public void Deposit(decimal amount)
        {
            this.MoneyOwed -= amount;
            this.Limit += amount;
        }
    }
}
