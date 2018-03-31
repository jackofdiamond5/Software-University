using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string input;
        ListyIterator<string> listy = new ListyIterator<string>(new List<string>());
        try
        {
            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split();
                var command = args[0];

                switch (command)
                {
                    case "Create":
                        if (args.Length > 1)
                        {
                            listy = new ListyIterator<string>(args.Skip(1).ToList());
                        }
                        break;
                    case "Print":
                        listy.Print();
                        break;
                    case "PrintAll":
                        listy.PrintAll();
                        Console.WriteLine();
                        break;
                    case "Move":
                        Console.WriteLine(listy.Move());
                        break;
                    case "HasNext":
                        Console.WriteLine(listy.HasNext());
                        break;
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
