using System;
using System.Collections.Generic;

public class Person
{
    private string name;
    private decimal money;
    private readonly List<Product> products;

    public Person()
    {
        this.products = new List<Product>();
    }

    public Person(string name, decimal money)
        : this()
    {
        this.Name = name;
        this.Money = money;
    }

    public string Name
    {
        get
        {
            return this.name;
        }

        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            this.name = value;
        }
    }

    public decimal Money
    {
        get
        {
            return this.money;
        }

        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }

            this.money = value;
        }
    }

    public IReadOnlyCollection<Product> Products => this.products.AsReadOnly();

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void SubstractMoney(decimal amount)
    {
        this.Money -= amount;
    }
}

