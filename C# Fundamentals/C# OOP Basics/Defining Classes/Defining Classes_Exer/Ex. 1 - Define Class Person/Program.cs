public class Program
{
    public static void Main()
    {
        var firstPerson = new Person
        {
            Age = 20,
            Name = "Pesho"
        };

        var secondPerson = new Person();
        secondPerson.Name = "Gosho";
        secondPerson.Age = 18;
    }
}

