using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var people = new HashSet<Person>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var args = input.Split();
            var personName = args[0];
            var command = args[1];

            var person = new Person(personName);
            if (people.All(p => p.Name != personName))
            {
                people.Add(person);
            }

            switch (command)
            {
                case "company":
                    var companyName = args[2];
                    var companyDepartment = args[3];
                    var salary = decimal.Parse(args[4]);

                    var company = new Company(companyName, companyDepartment, salary);
                    if (people.Any(p => p.Name == personName))
                    {
                        people.Single(p => p.Name == personName).Company = company;
                    }
                    break;
                case "pokemon":
                    var pokemonName = args[2];
                    var pokemonType = args[3];

                    var pokemon = new Pokemon(pokemonName, pokemonType);
                    if (people.Any(p => p.Name == personName))
                    {
                        people.Single(p => p.Name == personName).Pokemon.Add(pokemon);
                    }
                    break;
                case "parents":
                    var parentName = args[2];
                    var parentBirthday = args[3];

                    var parent = new Parent(parentName, parentBirthday);
                    if (people.Any(p => p.Name == personName))
                    {
                        people.Single(p => p.Name == personName).Parents.Add(parent);
                    }
                    break;
                case "children":
                    var childName = args[2];
                    var childBirthday = args[3];

                    var child = new Child(childName, childBirthday);
                    if (people.Any(p => p.Name == personName))
                    {
                        people.Single(p => p.Name == personName).Children.Add(child);
                    }
                    break;
                case "car":
                    var carModel = args[2];
                    var carSpeed = args[3];

                    var car = new Car(carModel, carSpeed);
                    if (people.Any(p => p.Name == personName))
                    {
                        people.Single(p => p.Name == personName).Car = car;
                    }
                    break;
            }
        }

        var persoonToLookup = Console.ReadLine();

        Console.WriteLine(people.Single(p => p.Name == persoonToLookup));
    }
}
