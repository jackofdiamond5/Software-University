public class Cyrmic : Cat
{
    private double furLength;

    public Cyrmic(string breed, string name, double furLength) : base(breed, name)
    {
        this.FurLength = furLength;
    }

    public double FurLength { get => furLength; set => furLength = value; }

    public override string ToString()
    {
        return $"{base.ToString()} {this.FurLength:F2}";
    }
}

