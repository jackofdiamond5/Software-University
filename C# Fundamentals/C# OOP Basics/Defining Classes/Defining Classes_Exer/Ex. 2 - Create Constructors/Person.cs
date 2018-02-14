public class Person
{
    public Person()
    {
        this.Name = "No name";
        this.Age = 1;
    }

    public Person(int age) : this()
    {
        this.Age = age;
    }

    public Person(int age, string name) : this(age)
    {
        this.Name = name;
        this.Age = age;
    }

    private string name;
    private int age;

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
}