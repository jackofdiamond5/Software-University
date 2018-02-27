using System;

public class Program
{
    static void Main()
    {
        var numbers = Console.ReadLine().Split();
        var urls = Console.ReadLine().Split();


        foreach (var number in numbers)
        {
            try
            {
                var smartphone = new Smartphone
                {
                    Number = number
                };

                Console.WriteLine($"Calling... {smartphone.Number}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        foreach (var url in urls)
        {
            try
            {
                var smartphone = new Smartphone
                {
                    Url = url
                };

                Console.WriteLine($"Browsing: {smartphone.Url}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}