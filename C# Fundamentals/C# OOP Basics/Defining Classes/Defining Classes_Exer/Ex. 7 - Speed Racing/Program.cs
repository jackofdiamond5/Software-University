using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());

        var cars = new List<Car>();

        for (int i = 0; i < n; i++)
        {
            var line = Console.ReadLine().Split();

            cars.Add(new Car
            {
                Model = line[0],
                FuelAmount = double.Parse(line[1]),
                FuelConsumption = double.Parse(line[2])
            });
        }

        var input = Console.ReadLine().Split();
        while (input[0] != "End")
        {
            var currentCarModel = input[1];
            var currentCarDistance = double.Parse(input[2]);

            var currentCar = cars.Where(c => c.Model == currentCarModel).ToList();

            if (currentCar[0].CanMove(currentCar[0], currentCarDistance))
            {
                Car.UpdateCar(ref cars, currentCarModel, currentCarDistance);
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }

            input = Console.ReadLine().Split();
        }

        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:F2} {car.DistanceTravelled}");
        }
    }
}
