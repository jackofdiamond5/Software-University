using System.Collections.Generic;

class Car
{
    public string Model { get; set; }

    public double FuelAmount { get; set; }
    
    public double FuelConsumption { get; set; }

    public double DistanceTravelled { get; set; }

    public bool CanMove(Car car, double distanceKm)
    {
        var fuelForDistance = distanceKm * car.FuelConsumption;

        return fuelForDistance <= FuelAmount;
    }

    public static void UpdateCar(ref List<Car> cars, string currentCarModel, double currentCarDistance)
    {
        foreach (var car in cars)
        {
            if (car.Model != currentCarModel)
                continue;

            car.DistanceTravelled += currentCarDistance;
            car.FuelAmount -= (car.FuelConsumption * currentCarDistance);
            break;
        }
    }
}

