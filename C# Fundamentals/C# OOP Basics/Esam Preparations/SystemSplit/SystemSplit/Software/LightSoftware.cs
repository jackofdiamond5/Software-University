public class LightSoftware : Software
{
    private const double lightDifferencial = 0.5;

    public LightSoftware(string name, int capacityConsumption, int memoryConsumption) 
        : base(name, capacityConsumption, memoryConsumption)
    {
        this.CapacityConsumption += (int)(this.CapacityConsumption * lightDifferencial);
        this.MemoryConsumption -= (int)(this.MemoryConsumption * lightDifferencial);
    }
}
