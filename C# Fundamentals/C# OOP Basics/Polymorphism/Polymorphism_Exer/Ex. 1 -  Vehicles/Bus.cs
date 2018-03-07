class Bus : Vehicle
{
    private const double ConsumptionIncrease = 1.4;

    public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) 
        : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
        this.HasPassengers = true;
    }

    public bool HasPassengers { get; set; }

    public override string Drive(double distance)
    {
        var currentConsumption = this.FuelConsumption;

        if (this.HasPassengers)
        {
            currentConsumption += ConsumptionIncrease;
        }

        var fuelToUse = distance * currentConsumption;
        var vehicleType = this.GetType().Name;

        if (fuelToUse > this.FuelQuantity)
        {
            return $"{vehicleType} needs refueling";
        }

        this.FuelQuantity -= fuelToUse;

        return $"{vehicleType} travelled {distance} km";
    }
}
