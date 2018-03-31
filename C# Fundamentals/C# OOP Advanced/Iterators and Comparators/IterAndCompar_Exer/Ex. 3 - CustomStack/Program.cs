using System;
using System.Linq;

public class Program
{
    static void Main()
    {
        string input;
        var cStack = new CustomStack<string>();
        try
        {
            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var command = args[0];

                switch (command)
                {
                    case "Push":
                        var elements = args.Skip(1).ToArray();
                        cStack.Push(elements);
                        break;
                    case "Pop":
                        cStack.Pop();
                        break;
                }
            }

            foreach (var item in cStack/*.Reverse()*/)
            {
                Console.WriteLine(item);
            }
            foreach (var item in cStack/*.Reverse()*/)
            {
                Console.WriteLine(item);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("No elements");
        }
    }
}
