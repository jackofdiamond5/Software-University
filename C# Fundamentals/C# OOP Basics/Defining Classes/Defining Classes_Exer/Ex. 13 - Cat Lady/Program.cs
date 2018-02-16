using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var cats = new List<Cat>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var data = input.Split();
            var breed = data[0];
            var name = data[1];
            var breedInfo = data[2];

            var cat = new Cat(breed, name);
            switch (breed)
            {
                case "Siamese":
                    cat = new Siamese(breed, name, int.Parse(breedInfo));
                    break;
                case "Cymric":
                    cat = new Cyrmic(breed, name, double.Parse(breedInfo));
                    break;
                case "StreetExtraordinaire":
                    cat = new StreetExtraordinaire(breed, name, int.Parse(breedInfo));
                    break;
            }

            cats.Add(cat);
        }

        var catName = Console.ReadLine();

        var catLookup = cats.SingleOrDefault(c => c.Name == catName);
        Console.WriteLine(catLookup);
    }
}

