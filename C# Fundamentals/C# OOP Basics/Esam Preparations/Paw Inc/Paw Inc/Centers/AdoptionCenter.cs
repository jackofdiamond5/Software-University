using System.Linq;
using System.Collections.Generic;

public class AdoptionCenter : Center
{
    public AdoptionCenter(string name)
        : base(name) { }

    public List<Animal> SendForCleansing()
    {
        return this.StoredAnimals.Where(a => a.IsCleansed == false).ToList();
    }

    public List<Animal> Adopt()
    {
        var animalsToAdopt = this.StoredAnimals.Where(a => a.IsCleansed == true).ToList();
        this.StoredAnimals.RemoveAll(a => a.IsCleansed == true);
      
        return animalsToAdopt;
    }

    public void RegisterAnimal(Animal animal)
    {
        this.StoredAnimals.Add(animal);
    }
}
