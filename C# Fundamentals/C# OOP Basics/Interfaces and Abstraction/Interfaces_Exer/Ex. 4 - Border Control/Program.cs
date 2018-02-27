using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var citizens = new List<ICitizen>();
        var petsAndHumans = new List<IAlive>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var args = input.Split();
            var command = args[0];

            switch (command)
            {
                case "Citizen":
                    var citizenName = args[1];
                    var citizenAge = int.Parse(args[2]);
                    var citizenId = args[3];
                    var citizenBirthdate = args[4];

                    var human = new Citizen(citizenName, citizenAge, citizenId, citizenBirthdate);

                    petsAndHumans.Add(human);
                    break;
                case "Pet":
                    var petName = args[1];
                    var petBirthdate = args[2];

                    var pet = new Pet(petName, petBirthdate);

                    petsAndHumans.Add(pet);
                    break;
                case "Robot":
                    var robotModel = args[1];
                    var robotId = args[2];

                    var robot = new Robot(robotModel, robotId);

                    citizens.Add(robot);
                    break;
            }
        }

        var birthdateToLookUp = Console.ReadLine();

        petsAndHumans
            .Where(e => e.Birthdate.Contains(birthdateToLookUp))
            .ToList()
            .ForEach(Console.WriteLine);

        //var idToLookUp = Console.ReadLine();

        //citizens
        //    .Where(c => c.Id.EndsWith(idToLookUp))
        //    .ToList()
        //    .ForEach(Console.WriteLine);
    }
}
