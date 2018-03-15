using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    private static Queue<Animal> adoptedAnimalsQueue = new Queue<Animal>();
    private static Queue<Animal> cleansedAnimalsQueue = new Queue<Animal>();

    static void Main()
    {
        string input;
        var centers = new List<Center>();
        var animals = new List<Animal>();

        while ((input = Console.ReadLine()) != "Paw Paw Pawah")
        {
            var args = input
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim())
                .ToArray();

            var command = args[0];

            switch (command)
            {
                case "RegisterCleansingCenter":
                    var newCleansingCenter = new CleansingCenter(args[1]);
                    centers.Add(newCleansingCenter);
                    break;
                case "RegisterAdoptionCenter":
                    var newAdoptionCenter = new AdoptionCenter(args[1]);
                    centers.Add(newAdoptionCenter);
                    break;
                case "RegisterDog":
                    RegisterDogCommand(args, centers);
                    break;
                case "RegisterCat":
                    RegisterCatCommand(args, centers);
                    break;
                case "SendForCleansing":
                    SendForCleansingCommand(args, centers);
                    break;
                case "Cleanse":
                    CleanseAnimalsCommand(args, centers);
                    break;
                case "Adopt":
                    AdoptAnimalsCommand(args, centers);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        Console.WriteLine();
        PrintResult(centers);
    }

    private static void PrintResult(List<Center> centers)
    {
        Console.WriteLine("Paw Incorporative Regular Statistics");
        Console.WriteLine(
            $"Adoption Centers: {centers.Where(c => c.GetType().Name == "AdoptionCenter").Count()}");
        Console.WriteLine($"Cleansing Centers: {centers.Where(c => c.GetType().Name == "CleansingCenter").Count()}");


        if (adoptedAnimalsQueue.Count == 0)
        {
            Console.WriteLine("Adopted Animals: None");
        }
        else
        {
            Console.WriteLine($"Adopted Animals: {string.Join(", ", adoptedAnimalsQueue.OrderBy(a => a.Name))}");
        }

        if (cleansedAnimalsQueue.Count == 0)
        {
            Console.WriteLine("Cleansed Animals: None");
        }
        else
        {
            Console.WriteLine($"Cleansed Animals: {string.Join(", ", cleansedAnimalsQueue.OrderBy(a => a.Name))}");
        }
        
        var cleanedAnimals = centers.
            Where(c => c.GetType().Name == "AdoptionCenter")
            .Select(c => new
            {
                CleanedAnimals = c.StoredAnimals.Where(a => a.IsCleansed == true).Count()
            }).Sum(r => r.CleanedAnimals);

        Console.WriteLine(
            $"Animals Awaiting Adoption: {cleanedAnimals}");
        Console.WriteLine(
            $"Animals Awaiting Cleansing: {centers.Where(c => c.GetType().Name == "CleansingCenter").Select(c => c.StoredAnimals.Count).Sum()}");
    }

    private static void AdoptAnimalsCommand(string[] args, List<Center> centers)
    {
        var adoptCenterName = args[1];
        var adoptionCenter = (AdoptionCenter)centers.FirstOrDefault(c => c.Name == adoptCenterName);

        var adoptedAnimalsList = adoptionCenter.Adopt();
        adoptedAnimalsList.ForEach(a => adoptedAnimalsQueue.Enqueue(a));
    }

    private static void CleanseAnimalsCommand(string[] args, List<Center> centers)
    {
        var cleansingCenterName = args[1];
        var cleansingCenter =
            (CleansingCenter)centers.FirstOrDefault(c => c.Name == cleansingCenterName);

        var cleansedAnimals = cleansingCenter.CleanseAnimals();

        foreach (var animal in cleansedAnimals)
        {
            var animalAdoptionCenter =
                centers.FirstOrDefault(c => c.Name == animal.AdoptionCenterName);
            animalAdoptionCenter.StoredAnimals.Add(animal);
            cleansedAnimalsQueue.Enqueue(animal);
        }
    }

    private static void SendForCleansingCommand(string[] args, List<Center> centers)
    {
        var adoptionCenterName = args[1];
        var cleansingCenterName = args[2];

        var adoptionCenter = centers.FirstOrDefault(c => c.Name == adoptionCenterName);
        var cleansingCenter = centers.FirstOrDefault(c => c.Name == cleansingCenterName);

        var animalsToCleanse = adoptionCenter
            .StoredAnimals
            .Where(a => a.IsCleansed == false)
            .ToList();

        cleansingCenter.StoredAnimals.AddRange(animalsToCleanse);
        adoptionCenter.StoredAnimals.RemoveAll(a => a.IsCleansed == false);
    }

    private static void RegisterCatCommand(string[] args, List<Center> centers)
    {
        var catName = args[1];
        var catAge = int.Parse(args[2]);
        var intelligence = int.Parse(args[3]);
        var adoptionCenterNameCat = args[4];

        var cat = new Cat(catName, catAge, adoptionCenterNameCat, intelligence);
        var centerForCat = centers.FirstOrDefault(c => c.Name == adoptionCenterNameCat) as AdoptionCenter;

        if (centerForCat != null)
        {
            centerForCat.RegisterAnimal(cat);
        }
    }

    private static void RegisterDogCommand(string[] args, List<Center> centers)
    {
        var dogName = args[1];
        var dogAge = int.Parse(args[2]);
        var learnedCommands = int.Parse(args[3]);
        var adoptionCenterNameDog = args[4];

        var dog = new Dog(dogName, dogAge, adoptionCenterNameDog, learnedCommands);
        var centerForDog =
            centers.FirstOrDefault(c => c.Name == adoptionCenterNameDog) as AdoptionCenter;

        if (centerForDog != null)
        {
            centerForDog.RegisterAnimal(dog);
        }
    }
}
