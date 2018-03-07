using System;

public class Dog : Mammal
{
    private const double WeightIncreaseAmount = 0.40;
    private const string FoodPreference = "Meat";

    public Dog(string name, double weight, string livingRegion) 
        : base(name, weight, livingRegion) { }
   
    public override string AskForFood()
    {
        return "Woof!";
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