public abstract class Animal
{
    protected Animal(string name, int age, string adoptionCenterName)
    {
        this.Name = name;
        this.Age = age;
        this.IsCleansed = false;
        this.AdoptionCenterName = adoptionCenterName;
    }

    public string Name { get; protected set; }

    public int Age { get; protected set; }

    public bool IsCleansed { get; set; }

    public string AdoptionCenterName { get; }

    public override string ToString()
    {
        return this.Name;
    }
}

