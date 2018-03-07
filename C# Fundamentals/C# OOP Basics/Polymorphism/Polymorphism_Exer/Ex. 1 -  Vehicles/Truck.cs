using System;

public class Truck : Vehicle
{
    private const double ConsumptionIncrease = 1.6;
    private const double LeakingAmount = 0.05;

    public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
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

    public override void Refuel(double fuelAmount)
    {
        if (fuelAmount <= 0)
        {
            throw new ArgumentException($"Fuel must be a positive number");
        }

        var tankAfterRefuel = this.FuelQuantity + fuelAmount - (fuelAmount * LeakingAmount);
        if (tankAfterRefuel > this.TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");
        }

        this.FuelQuantity = tankAfterRefuel;
    }
}