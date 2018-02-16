public class Car
{
    private string model;
    private string speed;

    public Car(string model, string speed)
    {
        this.Model = model;
        this.Speed = speed;
    }

    public string Model { get => model; set => model = value; }

    public string Speed { get => speed; set => speed = value; }

    public override string ToString()
    {
        return $"{this.Model} {this.Speed}";
    }
}