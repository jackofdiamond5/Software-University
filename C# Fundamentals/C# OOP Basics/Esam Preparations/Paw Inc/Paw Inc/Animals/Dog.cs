public class Dog : Animal
{
    public Dog(string name, int age, string adoptionCenterName, int learnedCommands)
        : base(name, age, adoptionCenterName)
    {
        this.LearnedCommands = learnedCommands;
    }

    public int LearnedCommands { get; private set; }
}

