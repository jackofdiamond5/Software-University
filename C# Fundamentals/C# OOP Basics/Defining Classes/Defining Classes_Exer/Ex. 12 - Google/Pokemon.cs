public class Pokemon
{
    private string name;
    private string type;

    public Pokemon(string name, string type)
    {
        this.Name = name;
        this.Type = type;
    }

    public string Name { get => name; set => name = value; }

    public string Type { get => type; set => type = value; }

    public override string ToString()
    {
        return $"{this.Name} {this.Type}";
    }
}