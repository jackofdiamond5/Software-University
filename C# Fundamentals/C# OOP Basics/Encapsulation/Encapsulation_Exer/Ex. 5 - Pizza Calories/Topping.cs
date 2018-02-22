using System;
using System.Linq;

class Topping
{
    private const int BaseCalories = 2;

    private readonly string[] Types = { "meat", "veggies", "cheese", "sauce" };
    private const double MinWeight = 1;
    private const double MaxWeight = 50;

    private const double MeatTypeVal = 1.2;
    private const double VeggiesTypeVal = 0.8;
    private const double CheeseTypeVal = 1.1;
    private const double SauseTypeVal = 0.9;

    private string type;
    private double weight;


    public Topping(string type, double weight)
    {
        this.Type = type;
        this.Weight = weight;
    }


    public string Type
    {
        get => type;
        private set
        {
            if (!Types.Contains(value.ToLower()))
            {
                throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }

            this.type = value;
        }
    }

    public double Weight
    {
        get => this.weight;
        private set
        {
            if (value < MinWeight || value > MaxWeight)
            {
                throw new ArgumentException($"{this.Type} weight should be in the range [{MinWeight}..{MaxWeight}].");
            }

            this.weight = value;
        }
    }

    public double Calories => this.CalculateCalories();

    private double CalculateCalories()
    {
        var toppingTypeVal = 0d;
        switch (this.Type.ToLower())
        {
            case "meat":
                toppingTypeVal = BaseCalories * (this.weight * MeatTypeVal);
                break;
            case "veggies":
                toppingTypeVal = BaseCalories * (this.weight * VeggiesTypeVal);
                break;
            case "cheese":
                toppingTypeVal = BaseCalories * (this.weight * CheeseTypeVal);
                break;
            case "sauce":
                toppingTypeVal = BaseCalories * (this.weight * SauseTypeVal);
                break;
        }

        return toppingTypeVal;
    }

}