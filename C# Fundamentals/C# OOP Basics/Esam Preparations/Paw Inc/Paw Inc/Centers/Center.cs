using System.Collections.Generic;

public abstract class Center
{
    protected Center(string name)
    {
        this.StoredAnimals = new List<Animal>();
        this.Name = name;
    }

    public string Name { get; protected set; }

    public List<Animal> StoredAnimals { get; protected set; }
}
