using System;
using System.Linq;

public class Program
{
    static void Main()
    {
        try
        {
            var pizzaName = string.Join("", Console.ReadLine().Split().Skip(1).ToArray());

            var doughData = Console.ReadLine().Split();
            var flourType = doughData[1];
            var technique = doughData[2];
            var doughWeight = double.Parse(doughData[3]);
            var dough = new Dough(flourType, technique, doughWeight);

            var pizza = new Pizza(pizzaName, dough);

            string input; 
            while ((input = Console.ReadLine()) != "END")
            {
                var command = input.Split();

                var toppingType = command[1];
                var weight = double.Parse(command[2]);
                var topping = new Topping(toppingType, weight);

                pizza.AddTopping(topping);
            }

            Console.WriteLine($"{pizza.Name} - {pizza.Calories:F2} Calories.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}