public class ExpressSoftware : Software
{
    public ExpressSoftware(string name, int capacityConsumption, int memoryConsumption) 
        : base(name, capacityConsumption, memoryConsumption)
    {
        this.MemoryConsumption = this.MemoryConsumption * 2;
    }
}
