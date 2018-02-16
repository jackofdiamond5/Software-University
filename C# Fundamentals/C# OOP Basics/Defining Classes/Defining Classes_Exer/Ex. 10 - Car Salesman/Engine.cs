using System.Text;

public class Engine
{
    private string model;
    private double power;
    private double displacement;
    private string efficiency;

    public Engine(string model, double power, double displacement, string efficiency)
    {
        this.Model = model;
        this.Power = power;
        this.Displacement = displacement;
        this.Efficiency = efficiency;
    }

    public string Model { get => model; set => model = value; }

    public double Power { get => power; set => power = value; }

    public double Displacement { get => displacement; set => displacement = value; }

    public string Efficiency { get => efficiency; set => efficiency = value; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"{this.Model}:");
        builder.AppendLine($"  Power: {this.Power}");

        switch (this.Displacement)
        {
            case 0:
                builder.AppendLine($"    Displacement: n/a");
                break;
            default:
                builder.AppendLine($"    Displacement: {this.Displacement}");
                break;
        }

        builder.AppendLine($"    Efficiency: {this.Efficiency ?? "n/a"}");

        return builder.ToString();
    }
}