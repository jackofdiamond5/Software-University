public class StreetExtraordinaire : Cat
{
    private int meowingDecibels;

    public StreetExtraordinaire(string breed, string name, int meowingDecibels) : base(breed, name)
    {
        this.MeowingDecibels = meowingDecibels;
    }

    public int MeowingDecibels { get => meowingDecibels; set => meowingDecibels = value; }

    public override string ToString()
    {
        return $"{base.ToString()} {this.MeowingDecibels}";
    }
}

