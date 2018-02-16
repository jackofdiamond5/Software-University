using System.Text;

public class Car
{
    private string model;
    private Engine engine;
    private double weight;
    private string color;

    public Car(string model, Engine engine, double weight, string color)
    {
        this.Model = model;
        this.Engine = engine;
        this.Weight = weight;
        this.Color = color;
    }

    public string Model { get => model; set => model = value; }

    public Engine Engine { get => engine; set => engine = value; }

    public double Weight { get => weight; set => weight = value; }

    public string Color { get => color; set => color = value; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"{this.Model}:");
        builder.Append($"  {this.Engine}");

        switch (this.Weight)
        {
            case 0:
                builder.AppendLine($" Weight: n/a");
                break;
            default:
                builder.AppendLine($" Weight: {this.Weight}");
                break;
        }

        builder.Append($" Color: {this.Color ?? "n/a"}");

        return builder.ToString();
    }
}