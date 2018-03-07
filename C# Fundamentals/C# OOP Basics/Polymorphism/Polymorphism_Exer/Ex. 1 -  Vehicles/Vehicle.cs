using System;

public abstract class Vehicle
{
    private double tankCapacity;
    private double fuelQuantity;
    private double fuelConsumption;

    public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
    {
        this.TankCapacity = tankCapacity;
        this.FuelQuantity = fuelQuantity;
        this.FuelConsumption = fuelConsumption;
    }

    public double FuelQuantity
    {
        get
        {
            return this.fuelQuantity;
        }
        protected set
        {
            if (value <= this.TankCapacity)
            {
                this.fuelQuantity = value;
            }
        }
    }

    public double FuelConsumption
    {
        get
        {
            return this.fuelConsumption;
        }
        protected set
        {
            this.fuelConsumption = value;
        }
    }

    public double TankCapacity
    {
        get
        {
            return this.tankCapacity;
        }
        protected set
        {
            this.tankCapacity = value;
        }
    }

    public abstract string Drive(double distance);

    public virtual void Refuel(double fuelAmount)
    {
        if (fuelAmount <= 0)
        {
            throw new ArgumentException($"Fuel must be a positive number");
        }

        var tankAfterRefuel = this.FuelQuantity + fuelAmount;
        if (tankAfterRefuel > this.TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");
        }

        this.FuelQuantity = tankAfterRefuel;
    }

    public override string ToString()
    {
        return $"{this.GetType().Name}: {this.FuelQuantity:F2}";
    }
}