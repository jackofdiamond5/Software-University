public class Child
{
    private string name;
    private string birthday;

    public Child(string name, string birthday)
    {
        this.Name = name;
        this.Birthday = birthday;
    }

    public string Name { get => name; set => name = value; }

    public string Birthday { get => birthday; set => birthday = value; }

    public override string ToString()
    {
        return $"{this.Name} {this.Birthday}";
    }
}