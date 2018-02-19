using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var carsAmount = int.Parse(Console.ReadLine());

        var carList = GetCars(carsAmount);
        
        var command = Console.ReadLine();
        OutputResult(command, carList);
    }

    public static void OutputResult(string command, List<Car> carList)
    {
        switch (command)
        {
            case "fragile":
                var fragileCargoCars = carList
                    .Where(c => c.Cargo.CargoType == "fragile" && c.Tires.Any(p => p.Pressure < 1))
                    .ToList();

                Console.WriteLine(string.Join("\n", fragileCargoCars));
                break;
            case "flamable":
                var flamableCargoCars = carList
                    .Where(c => c.Cargo.CargoType == "flamable" && c.Engine.Power > 250)
                    .ToList();

                Console.WriteLine(string.Join("\n", flamableCargoCars));
                break;
        }
    }

    public static List<Car> GetCars(int carsAmount)
    {
        var carList = new List<Car>();

        for (var i = 0; i < carsAmount; i++)
        {
            var args = Console.ReadLine().Split();

            var carModel = args[0];

            var engineSpeed = args[1];
            var enginePower = args[2];

            var cargoWeight = args[3];
            var cargoType = args[4];

            var firstTirePressure = args[5];
            var firstTireAge = args[6];
            var firstTire = new Tire(double.Parse(firstTirePressure), int.Parse(firstTireAge));

            var secondTirePressure = args[7];
            var secondTireAge = args[8];
            var secondTire = new Tire(double.Parse(secondTirePressure), int.Parse(secondTireAge));

            var thirdTirePressure = args[9];
            var thirdTireAge = args[10];
            var thirdTire = new Tire(double.Parse(thirdTirePressure), int.Parse(thirdTireAge));

            var fourthTirePressure = args[11];
            var fourthTireAge = args[12];
            var fourthTire = new Tire(double.Parse(fourthTirePressure), int.Parse(fourthTireAge));

            var carEngine = new Engine(int.Parse(engineSpeed), int.Parse(enginePower));
            var carCargo = new Cargo(int.Parse(cargoWeight), cargoType);
            var tires = new List<Tire> { firstTire, secondTire, thirdTire, fourthTire };

            var car = new Car(carModel, carEngine, carCargo, tires);
            carList.Add(car);
        }

        return carList;
    }
}

