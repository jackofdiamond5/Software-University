public class Hen : Bird
{
    private const double WeightIncreaseAmount = 0.35;

    public Hen(string name, double weight, double wingSize)
        : base(name, weight, wingSize) { }

    public override string AskForFood()
    {
        return "Cluck";
    }

    public override void Feed(Food food)
    {
        var weightToAdd = WeightIncreaseAmount * food.Quantity;
        this.Weight += weightToAdd;

        this.FoodEaten += food.Quantity;
    }
}