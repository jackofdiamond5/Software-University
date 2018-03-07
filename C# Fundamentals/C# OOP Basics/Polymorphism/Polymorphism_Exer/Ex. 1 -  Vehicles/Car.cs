public class Car : Vehicle
{
    private const double ConsumptionIncrease = 0.9;

    public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity) { }

    public override string Drive(double distance)
    {
        var fuelToUse = distance * (this.FuelConsumption + ConsumptionIncrease);
        var vehicleType = this.GetType().Name;

        if (fuelToUse > this.FuelQuantity)
        {
            return $"{vehicleType} needs refueling";
        }

        this.FuelQuantity -= fuelToUse;

        return $"{vehicleType} travelled {distance} km";
    }
}