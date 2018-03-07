using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] vehicleArgs = null;
        var vehicles = new List<Vehicle>();

        try
        {
            for (var i = 0; i < 3; i++)
            {
                vehicleArgs = Console.ReadLine().Split();
                var vehicleType = vehicleArgs[0];
                var initialFuelQuantity = double.Parse(vehicleArgs[1]);
                var fuelConsumption = double.Parse(vehicleArgs[2]);
                var tankCapacity = double.Parse(vehicleArgs[3]);

                var vehicle = CreateVehicle(initialFuelQuantity, fuelConsumption, tankCapacity, vehicleType);
                vehicles.Add(vehicle);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        var commandCount = int.Parse(Console.ReadLine());

        for (var i = 0; i < commandCount; i++)
        {
            var commandArgs = Console.ReadLine().Split();
            var commandType = commandArgs[0].Trim();
            var targetedVehicle = commandArgs[1].Trim();
            var valueAmount = double.Parse(commandArgs[2]);

            try
            {
                switch (commandType)
                {
                    case "Drive":
                        Console.WriteLine(vehicles.SingleOrDefault(v => v.GetType().Name == targetedVehicle).Drive(valueAmount));
                        break;
                    case "Refuel":
                        vehicles.SingleOrDefault(v => v.GetType().Name == targetedVehicle).Refuel(valueAmount);
                        break;
                    case "DriveEmpty":
                        var vehicle = vehicles.SingleOrDefault(v => v.GetType().Name == targetedVehicle);

                        Bus bus = vehicle as Bus;
                        if (bus != null)
                        {
                            bus.HasPassengers = false;
                            Console.WriteLine(bus.Drive(valueAmount));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        vehicles.ForEach(Console.WriteLine);
    }

    private static Vehicle CreateVehicle(double fuelQuantity, double fuelConsumption, double tankCapacity, string vehicleType)
    {
        switch (vehicleType)
        {
            case "Truck":
                return new Truck(fuelQuantity, fuelConsumption, tankCapacity);
            case "Car":
                return new Car(fuelQuantity, fuelConsumption, tankCapacity);
            case "Bus":
                return new Bus(fuelQuantity, fuelConsumption, tankCapacity);
            default:
                throw new Exception();
        }
    }
}