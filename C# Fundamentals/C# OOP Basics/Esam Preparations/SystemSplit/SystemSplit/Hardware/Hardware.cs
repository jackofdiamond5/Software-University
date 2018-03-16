using System.Linq;
using System.Collections.Generic;

public abstract class Hardware
{
    protected Hardware(string name, int maximumCapacity, int maximumMemory)
    {
        this.Name = name;
        this.CapacityForUsage = maximumCapacity;
        this.MemoryForUsage = maximumMemory;
        this.RegisteredSoftware = new List<Software>();
    }

    public string Name { get; protected set; }

    public int CapacityForUsage { get; protected set; }

    public int MemoryForUsage { get; protected set; }
    
    public int MaximumMemory { get; protected set; }

    public int MaximumCapacity { get; protected set; }

    public List<Software> RegisteredSoftware { get; protected set; }

    public virtual void RegisterSoftwareComponent(Software component)
    {
        this.CapacityForUsage -= component.CapacityConsumption;
        this.MemoryForUsage -= component.MemoryConsumption;

        this.RegisteredSoftware.Add(component);
    }

    public virtual Software ReleaseSoftware(string componentName)
    {
        var targetSoftware = this.RegisteredSoftware.SingleOrDefault(s => s.Name == componentName);
        if(targetSoftware != null)
        {
            this.CapacityForUsage  += targetSoftware.CapacityConsumption;
            this.MemoryForUsage += targetSoftware.MemoryConsumption;
            this.RegisteredSoftware.Remove(targetSoftware);

            return targetSoftware;
        }

        return null;
    }

    public bool CanRunSoftware(Software component)
    {
        var canYouRunIt = this.CapacityForUsage - component.CapacityConsumption >= 0 &&
                       this.MemoryForUsage - component.MemoryConsumption >= 0;

        return canYouRunIt;
    }
}
