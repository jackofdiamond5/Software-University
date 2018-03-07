public abstract class Animal
{
    public Animal(string name, double weight)
    {
        this.Name = name;
        this.Weight = weight;
    }

    public string Name { get; protected set; }

    public double Weight { get; protected set; }

    public int FoodEaten { get; set; }

    public abstract string AskForFood();

    public abstract void Feed(Food food);
}