public class Tomcat : Animal
{
    public Tomcat(string name, int age, string gender)
        : base(name, age, "Male") { }

    public override string ProduceSound()
    {
        return "MEOW";
    }
}

