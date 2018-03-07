using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var animals = new Stack<Animal>();

        var lineCounter = 0;
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var args = input.Split();
            try
            {
                if (lineCounter % 2 == 0)
                {
                    var animal = CreateAnimal(args);
                    Console.WriteLine(animal.AskForFood());
                    animals.Push(animal);
                }
                else
                {
                    var food = CreateFood(args);
                    var animalToFeed = animals.Peek();

                    animalToFeed.Feed(food);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            lineCounter++;
        }

        animals
            .Reverse()
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private static Food CreateFood(string[] args)
    {
        var foodType = args[0];
        var foodQuantity = int.Parse(args[1]);

        switch (foodType)
        {
            case "Vegetable":
                return new Vegetable(foodQuantity);
            case "Fruit":
                return new Fruit(foodQuantity);
            case "Meat":
                return new Meat(foodQuantity);
            case "Seeds":
                return new Seeds(foodQuantity);
            default:
                throw new ArgumentException();
        }
    }

    private static Animal CreateAnimal(string[] args)
    {
        var animalType = args[0];
        var name = args[1];
        var weight = double.Parse(args[2]);

        switch (animalType)
        {
            case "Owl":
                var owlWingSize = double.Parse(args[3]);
                return new Owl(name, weight, owlWingSize);
            case "Hen":
                var henWingSize = double.Parse(args[3]);
                return new Hen(name, weight, henWingSize);
            case "Mouse":
                var mouseLivingRegion = args[3];
                return new Mouse(name, weight, mouseLivingRegion);
            case "Dog":
                var dogLivingRegion = args[3];
                return new Dog(name, weight, dogLivingRegion);
            case "Cat":
                var catLivingRegion = args[3];
                var catBreed = args[4];
                return new Cat(name, weight, catLivingRegion, catBreed);
            case "Tiger":
                var tigerLivingRegion = args[3];
                var tigerBreed = args[4];
                return new Tiger(name, weight, tigerLivingRegion, tigerBreed);
            default:
                throw new ArgumentException();
        }
    }
}
