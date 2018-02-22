using System;
using System.Linq;
using System.Collections.Generic;

class Pizza
{
    private const int MinNameLength = 1;
    private const int MaxNameLength = 15;

    private const int MinToppingsCount = 0;
    private const int MaxTopingsCount = 10;

    private string name;
    private Dough dough;
    private List<Topping> toppings;
    private int toppingsCount;


    public Pizza(string name, Dough dough)
    {
        this.Name = name;
        this.Dough = dough;
        this.toppings = new List<Topping>();
    }


    public string Name
    {
        get
        {
            return this.name;
        }

        private set
        {
            if (value.Length < MinNameLength || value.Length > MaxNameLength)
            {
                throw new ArgumentException(
                    $"Pizza name should be between {MinNameLength} and {MaxNameLength} symbols.");
            }

            this.name = value;
        }
    }

    public Dough Dough
    {
        get
        {
            return this.dough;
        }

        private set => this.dough = value;
    }

    public int ToppingsCount
    {
        get
        {
            if (this.toppings.Count > 10)
            {
                throw new ArgumentException(
                    $"Number of toppings should be in range [{MinToppingsCount}..{MaxTopingsCount}].");
            }

            return this.toppingsCount;
        }
    }

    public double Calories => CalculateCalories();

    public void AddTopping(Topping topping)
    {
        toppings.Add(topping);
        this.toppingsCount = this.ToppingsCount;
    }

    private double CalculateCalories()
    {
        var toppingsCalories = toppings.Sum(t => t.Calories);

        return toppingsCalories + dough.Calories;
    }
}