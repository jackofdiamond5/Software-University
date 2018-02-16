public class Cat
{
    private string breed;
    private string name;

    public Cat(string breed, string name)
    {
        this.Breed = breed;
        this.Name = name;
    }

    public string Breed { get => breed; set => breed = value; }

    public string Name { get => name; set => name = value; }

    public override string ToString()
    {
        return $"{this.breed} {this.name}";
    }
}