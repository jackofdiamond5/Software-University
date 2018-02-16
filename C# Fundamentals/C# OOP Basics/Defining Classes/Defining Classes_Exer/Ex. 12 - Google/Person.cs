using System.Collections.Generic;
using System.Text;

public class Person
{
    private string name;
    private Company company;
    private List<Pokemon> pokemon;
    private List<Parent> parents;
    private List<Child> children;
    private Car car;

    public Person(string name)
    {
        this.Name = name;
        this.Pokemon = new List<Pokemon>();
        this.Parents = new List<Parent>();
        this.Children = new List<Child>();
    }

    public string Name { get => name; set => name = value; }

    public Company Company { get => company; set => company = value; }

    public List<Pokemon> Pokemon { get => pokemon; set => pokemon = value; }

    public List<Parent> Parents { get => parents; set => parents = value; }

    public List<Child> Children { get => children; set => children = value; }
    public Car Car { get => car; set => car = value; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine(this.Name);

        builder.AppendLine("Company:");
        if (this.Company != null)
        {
            builder.AppendLine(this.Company.ToString());
        }

        builder.AppendLine("Car:");
        if (this.Car != null)
        {
            builder.AppendLine(this.Car.ToString());
        }

        builder.AppendLine("Pokemon:");
        foreach (var pok in this.Pokemon)
        {
            builder.AppendLine(pok.ToString());
        }

        builder.AppendLine("Parents:");
        foreach (var parent in this.Parents)
        {
            builder.AppendLine(parent.ToString());
        }

        builder.AppendLine("Children:");
        foreach (var child in children)
        {
            builder.AppendLine(child.ToString());
        }

        return builder.ToString();
    }
}

