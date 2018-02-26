using System;

public class Program
{
    static void Main()
    {
        string animalType;
        while ((animalType = Console.ReadLine()) != "Beast!")
        {
            var input = Console.ReadLine().Split();
            try
            {
                switch (animalType)
                {
                    case "Cat":
                        var cat = new Cat(input[0], int.Parse(input[1]), input[2]);
                        Console.WriteLine(cat);
                        break;
                    case "Dog":
                        var dog = new Dog(input[0], int.Parse(input[1]), input[2]);
                        Console.WriteLine(dog);
                        break;
                    case "Frog":
                        var frog = new Frog(input[0], int.Parse(input[1]), input[2]);
                        Console.WriteLine(frog);
                        break;
                    case "Kitten":
                        var kitten = new Kitten(input[0], int.Parse(input[1]), input[2]);
                        Console.WriteLine(kitten);
                        break;
                    case "Tomcat":
                        var tomcat = new Tomcat(input[0], int.Parse(input[1]), input[2]);
                        Console.WriteLine(tomcat);
                        break;
                    default:
                        throw new ArgumentException("Invalid input!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    private static Animal GetAnimal(string input)
    {
        var args = input.Split();
        var animalName = args[0];
        var animalAge = int.Parse(args[1]);
        var animalGender = args[2];

        var animal = new Animal(animalName, animalAge, animalGender);

        return animal;
    }
}

