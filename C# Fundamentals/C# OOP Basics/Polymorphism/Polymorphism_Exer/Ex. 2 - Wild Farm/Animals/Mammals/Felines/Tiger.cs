using System;

public class Tiger : Feline
{
    private const double WeightIncreaseAmount = 1.00;
    private const string FoodPreference = "Meat";

    public Tiger(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion, breed) { }

    public override string AskForFood()
    {
        return "ROAR!!!";
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