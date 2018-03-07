using System;
using System.Linq;

public class Cat : Feline
{
    private const double WeightIncreaseAmount = 0.30;
    private string[] FoodPreference = new string[] { "Meat", "Vegetable" };

    public Cat(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion, breed) { }

    public override string AskForFood()
    {
        return "Meow";
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