using System;
using System.Linq;

public class Mouse : Mammal
{
    private const double WeightIncreaseAmount = 0.10;
    private string[] FoodPreference = new string[] { "Vegetable", "Fruit" };

    public Mouse(string name, double weight, string livingRegion)
        : base(name, weight, livingRegion) { }

    public override string AskForFood()
    {
        return "Squeak";
    }

    public override void Feed(Food food)
    {
        var foodType = food.GetType().Name;

        if (!FoodPreference.Contains(foodType))
        {
            throw new ArgumentException($"{this.GetType().Name} does not eat {foodType}!");
        }

        var weightToAdd = WeightIncreaseAmount * food.Quantity;
        this.Weight += weightToAdd;

        this.FoodEaten += food.Quantity;
    }
}