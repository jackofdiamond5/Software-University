using System;

public class Program
{
    static void Main()
    {
        string input;
        var interpreter = new CommandInterpreter();
        while ((input = Console.ReadLine()) != "END")
        {
            var commandArgs = input.Split();
            interpreter.InterpretCommand(commandArgs);
        }
    }
}
