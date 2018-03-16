public class PowerHardware : Hardware
{
    private const double powerDifferencial = 0.75;

    public PowerHardware(string name, int maximumCapacity, int maximumMemory) 
        : base(name, maximumCapacity, maximumMemory)
    {
        this.CapacityForUsage -= (int)(this.CapacityForUsage * powerDifferencial);
        this.MemoryForUsage += (int)(this.MemoryForUsage * powerDifferencial);

        this.MaximumMemory = this.MemoryForUsage;
        this.MaximumCapacity = this.CapacityForUsage;
    }
}