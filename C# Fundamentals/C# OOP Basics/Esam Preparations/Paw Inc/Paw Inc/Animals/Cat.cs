public class Cat : Animal
{
    public Cat(string name, int age, string adoptionCenterName, int intelligence) 
        : base(name, age, adoptionCenterName)
    {
        this.Intelligence = intelligence;
    }
    
    public int Intelligence { get; private set; }
}

