using System;

public class CommandInterpreter
{
    CustomList<string> list;

    public CommandInterpreter()
    {
        this.list = new CustomList<string>();
    }

    public void InterpretCommand(string[] args)
    {
        var command = args[0];

        switch (command)
        {
            case "Add":
                list.Add(args[1]);
                break;
            case "Sort":
                var sorted = Sorter.Sort<string>(list);
                list = sorted;
                break;
            case "Remove":
                var index = int.Parse(args[1]);
                list.Remove(index);
                break;
            case "Contains":
                var result = list.Contains(args[1]) ? "True" : "False";
                Console.WriteLine(result);
                break;
            case "Swap":
                var firstIndex = int.Parse(args[1]);
                var secondIndex = int.Parse(args[2]);
                list.Swap(firstIndex, secondIndex);
                break;
            case "Greater":
                Console.WriteLine(list.CountGreaterThan(args[1]));
                break;
            case "Max":
                Console.WriteLine(list.Max());
                break;
            case "Min":
                Console.WriteLine(list.Min());
                break;
            case "Print":
                foreach (var element in list)
                {
                    Console.WriteLine(element);
                }
                break;
            default:
                throw new ArgumentException();
        }
    }
}
