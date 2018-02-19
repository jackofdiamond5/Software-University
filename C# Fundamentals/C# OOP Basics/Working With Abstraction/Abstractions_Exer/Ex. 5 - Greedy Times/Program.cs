using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        long bagCapacity = long.Parse(Console.ReadLine());
        string[] itemsInSafe = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var bag = new Dictionary<string, Dictionary<string, long>>();
        long gold = 0;
        long stones = 0;
        long cash = 0;

        for (int i = 0; i < itemsInSafe.Length; i += 2)
        {
            string currentItemName = itemsInSafe[i];
            long currentItemCount = long.Parse(itemsInSafe[i + 1]);

            string itemType = GetItemType(currentItemName, currentItemCount, bagCapacity, bag);

            if (SkipItem(itemType, bag, bagCapacity, currentItemCount)) continue;  

            switch (itemType)
            {
                case "Gem":
                    if (!bag.ContainsKey(itemType))
                    {
                        if (bag.ContainsKey("Gold"))
                        {
                            if (currentItemCount > bag["Gold"].Values.Sum())
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (bag[itemType].Values.Sum() + currentItemCount > bag["Gold"].Values.Sum())
                    {
                        continue;
                    }
                    break;
                case "Cash":
                    if (!bag.ContainsKey(itemType))
                    {
                        if (bag.ContainsKey("Gem"))
                        {
                            if (currentItemCount > bag["Gem"].Values.Sum())
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (bag[itemType].Values.Sum() + currentItemCount > bag["Gem"].Values.Sum())
                    {
                        continue;
                    }
                    break;
            }

            long[] resultData =
                CalculateResultData(bag, itemType, currentItemName, currentItemCount, gold, stones, cash);
            gold = resultData[0];
            stones = resultData[1];
            cash = resultData[2];
        }

        PrintResult(bag);
    }

    public static bool SkipItem(string itemType, Dictionary<string, Dictionary<string, long>> bag,
        long bagCapacity, long currentItemCount)
    {
        return itemType == string.Empty ||
               bagCapacity < bag.Values.Select(x => x.Values.Sum()).Sum() + currentItemCount;
    }

    public static long[] CalculateResultData(Dictionary<string, Dictionary<string, long>> bag, string itemType,
        string currentItemName, long currentItemCount, long gold, long stones, long cash)
    {
        if (!bag.ContainsKey(itemType))
        {
            bag[itemType] = new Dictionary<string, long>();
        }

        if (!bag[itemType].ContainsKey(currentItemName))
        {
            bag[itemType][currentItemName] = 0;
        }

        bag[itemType][currentItemName] += currentItemCount;
        if (itemType == "Gold")
        {
            gold += currentItemCount;
        }
        else if (itemType == "Gem")
        {
            stones += currentItemCount;
        }
        else if (itemType == "Cash")
        {
            cash += currentItemCount;
        }

        return new[] { gold, cash, stones };
    }

    public static void PrintResult(Dictionary<string, Dictionary<string, long>> bag)
    {
        foreach (var item in bag)
        {
            Console.WriteLine($"<{item.Key}> ${item.Value.Values.Sum()}");
            foreach (var item2 in item.Value.OrderByDescending(y => y.Key).ThenBy(y => y.Value))
            {
                Console.WriteLine($"##{item2.Key} - {item2.Value}");
            }
        }
    }

    public static string GetItemType(string currentItemName, long currentItemCount,
        long bagCapacity, Dictionary<string, Dictionary<string, long>> bag)
    {
        string itemType = string.Empty;
        if (currentItemName.Length == 3)
        {
            itemType = "Cash";
        }
        else if (currentItemName.ToLower().EndsWith("gem"))
        {
            itemType = "Gem";
        }
        else if (currentItemName.ToLower() == "gold")
        {
            itemType = "Gold";
        }

        return itemType;
    }
}