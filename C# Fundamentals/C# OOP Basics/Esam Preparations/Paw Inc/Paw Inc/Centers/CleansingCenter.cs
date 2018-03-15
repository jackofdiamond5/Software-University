using System.Linq;
using System.Collections.Generic;

public class CleansingCenter : Center
{
    public CleansingCenter(string name) 
        : base(name) { }

    public List<Animal> CleanseAnimals()
    {
        var animalsToCleanse = this.StoredAnimals.Where(a => a.IsCleansed == false).ToList();
        animalsToCleanse.ForEach(a => a.IsCleansed = true);

        this.StoredAnimals.Clear();
        
        return animalsToCleanse;
    }
}
