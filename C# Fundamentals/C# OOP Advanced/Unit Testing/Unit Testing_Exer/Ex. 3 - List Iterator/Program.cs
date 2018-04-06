using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        ListIterator listIterator = null;

        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            var args = input.Split();
            var command = args[0];
            try
            {
                switch (command)
                {
                    case "Create":
                        listIterator = 
                            new ListIterator(args.Skip(1).ToList());
                        break;
                    case "Print":
                        listIterator.Print();
                        break;
                    case "HasNext":
                        Console.WriteLine(listIterator.HasNext());
                        break;
                    case "Move":
                        Console.WriteLine(listIterator.Move());
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

