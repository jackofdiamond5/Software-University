using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Dictionary<string, List<string>> doctors = new Dictionary<string, List<string>>();
        Dictionary<string, List<List<string>>> departments = new Dictionary<string, List<List<string>>>();

        string command;
        while ((command = Console.ReadLine()) != "Output")
        {
            string[] arguments = command.Split();
            var departament = arguments[0];
            var doctorFirstName = arguments[1];
            var doctorLastName = arguments[2];
            var doctorFullName = doctorFirstName + doctorLastName;
            var patientName = arguments[3];

            if (!doctors.ContainsKey(doctorFirstName + doctorLastName))
            {
                doctors[doctorFullName] = new List<string>();
            }
            if (!departments.ContainsKey(departament))
            {
                departments[departament] = new List<List<string>>();
                for (int stai = 0; stai < 20; stai++)
                {
                    departments[departament].Add(new List<string>());
                }
            }

            bool roomAvailable = departments[departament].SelectMany(x => x).Count() < 60;
            if (roomAvailable)
            {
                int room = 0;
                doctors[doctorFullName].Add(patientName);
                for (int currentRoom = 0; currentRoom < departments[departament].Count; currentRoom++)
                {
                    if (departments[departament][currentRoom].Count < 3)
                    {
                        room = currentRoom;
                        break;
                    }
                }

                departments[departament][room].Add(patientName);
            }
        }

        while ((command = Console.ReadLine()) != "End")
        {
            string[] args = command.Split();
            string doctorFullName = args[0];

            switch (args.Length)
            {
                case 1:
                    {
                        Console.WriteLine(
                            string.Join("\n", departments[doctorFullName]
                                .Where(x => x.Count > 0)
                                .SelectMany(x => x)));
                        break;
                    }

                case 2 when int.TryParse(args[1], out int room):
                    {
                        string departmentName = args[0];

                        Console.WriteLine(
                            string.Join("\n", departments[departmentName][room - 1].OrderBy(x => x)));
                        break;
                    }

                default:
                    {
                        string doctorName = args[0] + args[1];
                        Console.WriteLine(
                            string.Join("\n", doctors[doctorName].OrderBy(x => x)));
                        break;
                    }
            }
        }
    }
}