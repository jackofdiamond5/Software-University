public class HeavyHardware : Hardware
{
    private const double heavyDifferencial = 0.25;

    public HeavyHardware(string name, int maximumCapacity, int maximumMemory)
        : base(name, maximumCapacity, maximumMemory)
    {
        this.MemoryForUsage -= (int)(this.MemoryForUsage * heavyDifferencial);
        this.CapacityForUsage = this.CapacityForUsage * 2;

        this.MaximumMemory = this.MemoryForUsage;
        this.MaximumCapacity = this.CapacityForUsage;
    }
}

