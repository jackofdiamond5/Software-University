using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var soldiers = new List<ISoldier>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var soldierInfo = input.Split();
            var command = soldierInfo[0];

            try
            {
                switch (command)
                {
                    case "Private":
                        var priv = GetPrivateData(soldierInfo);
                        soldiers.Add(priv);
                        break;
                    case "LeutenantGeneral":
                        var general = GetGeneralData(soldierInfo, soldiers);
                        soldiers.Add(general);
                        break;
                    case "Engineer":
                        var engineer = GetEngineerData(soldierInfo);
                        soldiers.Add(engineer);
                        break;
                    case "Commando":
                        var commando = GetCommandoData(soldierInfo);
                        soldiers.Add(commando);
                        break;
                    case "Spy":
                        var spy = GetSpyData(soldierInfo);
                        soldiers.Add(spy);
                        break;
                }
            }
            catch (Exception)
            {
                continue;
            }
        }

        foreach (var soldier in soldiers)
        {
            Console.WriteLine(soldier);
        }
    }

    private static Spy GetSpyData(string[] soldierInfo)
    {
        var id = soldierInfo[1];
        var firstName = soldierInfo[2];
        var lastName = soldierInfo[3];
        var codeNumbers = int.Parse(soldierInfo[4]);

        var spy = new Spy(id, firstName, lastName, codeNumbers);

        return spy;
    }

    private static Commando GetCommandoData(string[] soldierInfo)
    {
        var id = soldierInfo[1];
        var firstName = soldierInfo[2];
        var lastName = soldierInfo[3];
        var salary = double.Parse(soldierInfo[4]);
        var corps = soldierInfo[5];

        var commando = new Commando(id, firstName, lastName, salary, corps);

        var missions = soldierInfo.Skip(6).ToArray();

        for (var i = 0; i < missions.Length - 1; i += 2)
        {
            var missionName = missions[i];
            var missionState = missions[i + 1];

            try
            {
                var mission = new Mission(missionName, missionState);
                commando.Missions.Add(mission);
            }
            catch (Exception) { }

        }

        return commando;
    }

    private static Engineer GetEngineerData(string[] soldierInfo)
    {
        var id = soldierInfo[1];
        var firstName = soldierInfo[2];
        var lastName = soldierInfo[3];
        var salary = double.Parse(soldierInfo[4]);
        var corps = soldierInfo[5];

        var engineer = new Engineer(id, firstName, lastName, salary,
            corps);

        var repairs = soldierInfo.Skip(6).ToArray();

        for (var i = 0; i < repairs.Length - 1; i += 2)
        {
            var partName = repairs[i];
            var repairTime = int.Parse(repairs[i + 1]);

            var repair = new Repair(partName, repairTime);

            engineer.Repairs.Add(repair);
        }

        return engineer;
    }

    private static LeutenantGeneral GetGeneralData(string[] soldierInfo, List<ISoldier> soldiers)
    {
        var id = soldierInfo[1];
        var first = soldierInfo[2];
        var last = soldierInfo[3];
        var salary = double.Parse(soldierInfo[4]);

        var general = new LeutenantGeneral(id, first, last, salary);

        var generalSubordinatesIds = soldierInfo.Skip(5).ToArray();
        if (soldiers.Count > 0)
        {
            soldiers
                .Where(p => generalSubordinatesIds.Contains(p.Id))
                .ToList()
                .ForEach(p => general.AddPrivate(p));
        }

        return general;
    }

    private static Private GetPrivateData(string[] soldierInfo)
    {
        var id = soldierInfo[1];
        var firstName = soldierInfo[2];
        var lastName = soldierInfo[3];
        var salary = double.Parse(soldierInfo[4]);

        return new Private(id, firstName, lastName, salary);
    }
}