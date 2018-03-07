using System;

public class Owl : Bird
{
    private const double WeightIncreaseAmount = 0.25;
    private const string FoodPreference = "Meat";

    public Owl(string name, double weight, double wingSize)
        : base(name, weight, wingSize) { }

    public override string AskForFood()
    {
        return "Hoot Hoot";
    }

    public override void Feed(Food food)
    {
        var foodType = food.GetType().Name;

        if (foodType != FoodPreference)
        {
            throw new ArgumentException($"{this.GetType().Name} does not eat {foodType}!");
        }

        var weightToAdd = WeightIncreaseAmount * food.Quantity;
        this.Weight += weightToAdd;

        this.FoodEaten += food.Quantity;
    }
}