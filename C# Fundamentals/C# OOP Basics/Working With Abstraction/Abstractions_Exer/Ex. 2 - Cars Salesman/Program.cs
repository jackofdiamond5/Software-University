using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var enginesCount = int.Parse(Console.ReadLine());
        var engines = GetEngines(enginesCount);
        
        var carsCount = int.Parse(Console.ReadLine());
        var cars = GetCars(carsCount, engines);
        
        PrintResult(cars);
    }

    public static void PrintResult(List<Car> cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }

    public static List<Car> GetCars(int carsCount, List<Engine> engines)
    {
        var cars = new List<Car>();

        for (var i = 0; i < carsCount; i++)
        {
            var carArgs = Console.ReadLine().Trim().Split();
            var carModel = carArgs[0];
            var carEngine = carArgs[1];

            double carWeight = 0;
            string carColor = null;
            if (carArgs.Length == 4)
            {
                carWeight = double.Parse(carArgs[2]);
                carColor = carArgs[3];
            }
            else if (carArgs.Length == 3)
            {
                var parsed = double.TryParse(carArgs[2], out var result);
                if (parsed)
                {
                    carWeight = result;
                }
                else
                {
                    carColor = carArgs[2];
                }
            }

            var currentCarEngine = engines.SingleOrDefault(e => e.Model == carEngine);
            var car = new Car(carModel, currentCarEngine, carWeight, carColor);
            cars.Add(car);
        }

        return cars;
    }

    public static List<Engine> GetEngines(int enginesCount)
    {
        var engines = new List<Engine>();

        for (var i = 0; i < enginesCount; i++)
        {
            var engineArgs = Console.ReadLine().Trim().Split();
            var engineModel = engineArgs[0];
            var enginePower = double.Parse(engineArgs[1]);

            double engineDisplacement = 0;
            string engineEfficiency = null;
            if (engineArgs.Length == 4)
            {
                engineDisplacement = double.Parse(engineArgs[2]);
                engineEfficiency = engineArgs[3];
            }
            else if (engineArgs.Length == 3)
            {
                var parsed = double.TryParse(engineArgs[2], out var result);
                if (parsed)
                {
                    engineDisplacement = result;
                }
                else
                {
                    engineEfficiency = engineArgs[2];
                }
            }

            var engine = new Engine(engineModel, enginePower, engineDisplacement, engineEfficiency);
            engines.Add(engine);
        }

        return engines;
    }

}