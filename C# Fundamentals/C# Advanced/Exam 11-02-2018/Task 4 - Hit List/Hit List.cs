using System;
using System.Linq;
using System.Collections.Generic;

namespace Task_4___Hit_List
{
    public class HitList
    {
        public static void Main()
        {
            var targetInfoIndex = int.Parse(Console.ReadLine());

            var repo = new Dictionary<string, Dictionary<string, string>>();

            string input;
            while ((input = Console.ReadLine().Trim()) != "end transmissions")
            {
                var data = input.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                var name = data[0];

                var kvps = data[1].Split(';');

                foreach (var kvp in kvps)
                {
                    var kvpData = kvp.Split(':');
                    var statKey = kvpData[0];
                    var statValue = kvpData[1];

                    if (!repo.ContainsKey(name))
                    {
                        repo.Add(name, new Dictionary<string, string>());
                        repo[name].Add(statKey, statValue);
                    }

                    repo[name][statKey] = statValue;
                }

            }

            var kill = Console.ReadLine().Split();
            var killName = kill[1];

            var infoIndex = 0;
            var personToKill = repo.Where(p => p.Key == killName).ToArray();
            Console.WriteLine($"Info on {killName}:");
            foreach (var kvp in personToKill)
            {
                foreach (var personInfo in kvp.Value.OrderBy(n => n.Key))
                {
                    Console.WriteLine($"---{personInfo.Key}: {personInfo.Value}");
                    infoIndex += personInfo.Key.Length + personInfo.Value.Length;
                }
            }

            Console.WriteLine($"Info index: {infoIndex}");

            if (infoIndex >= targetInfoIndex)
            {
                Console.WriteLine("Proceed");
            }
            else
            {
                Console.WriteLine($"Need {targetInfoIndex - infoIndex} more info.");
            }
        }
    }
}
