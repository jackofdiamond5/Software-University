using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var peopleCount = int.Parse(Console.ReadLine());

        var people = new List<IBuyer>();

        for (var i = 0; i < peopleCount; i++)
        {
            var data = Console.ReadLine().Split();

            switch (data.Length)
            {
                case 4:
                    var citizenName = data[0];
                    var citizenAge = int.Parse(data[1]);
                    var citizenId = data[2];
                    var birthdate = data[3];

                    var citizen = new Citizen(citizenName, citizenAge, citizenId, birthdate);

                    people.Add(citizen);
                    break;
                case 3:
                    var rebelName = data[0];
                    var rebelAge = int.Parse(data[1]);
                    var rebelGroup = data[2];

                    var rebel = new Rebel(rebelName, rebelAge, rebelGroup);

                    people.Add(rebel);
                    break;
                default:
                    throw new Exception();
            }
        }

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var buyerInput = input;

            people
                .Where(p => p.Name == buyerInput)
                .ToList()
                .ForEach(p => p.BuyFood());
        }

        Console.WriteLine(people.Sum(p => p.Food));
    }
}