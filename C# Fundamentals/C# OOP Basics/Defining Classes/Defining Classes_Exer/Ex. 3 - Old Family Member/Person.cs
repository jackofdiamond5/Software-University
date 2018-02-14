public class Person
{
    private string name;
    private int age;

    public Person(int age, string name)
    {
        this.Name = name;
        this.Age = age;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public override string ToString()
    {
        return $"{this.Name} {this.Age}";
    }
}