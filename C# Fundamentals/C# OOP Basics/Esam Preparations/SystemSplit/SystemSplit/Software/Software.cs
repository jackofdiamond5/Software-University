public abstract class Software
{
    protected Software(string name, int capacityConsumption, int memoryConsumption)
    {
        this.Name = name;
        this.CapacityConsumption = capacityConsumption;
        this.MemoryConsumption = memoryConsumption;
    }

    public string Name { get; protected set; }

    public int CapacityConsumption { get; protected set; }

    public int MemoryConsumption { get; protected set; }

    public override string ToString()
    {
        return this.Name;
    }
}
