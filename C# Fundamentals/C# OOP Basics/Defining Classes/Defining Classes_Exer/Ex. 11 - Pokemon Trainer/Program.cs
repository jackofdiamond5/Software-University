using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var trainers = new List<Trainer>();
        
        string input;
        while ((input = Console.ReadLine()) != "Tournament")
        {
            var args = input.Split();
            var trainerName = args[0];
            var pokemonName = args[1];
            var pokemonElement = args[2];
            var pokemonHealth = double.Parse(args[3]);

            var curPokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);

            if (trainers.Count != 0 && trainers.Any(t => t.Name == trainerName))
            {
                trainers.SingleOrDefault(t => t.Name == trainerName)?.Pokemon.Add(curPokemon);
            }
            else
            {
                var trainer = new Trainer(trainerName);
                trainer.Pokemon.Add(curPokemon);
                trainers.Add(trainer);
            }
        }

        string gameCommand;
        while ((gameCommand = Console.ReadLine()) != "End")
        {
            foreach (var trainer in trainers)
            {
                if (trainer.Pokemon.Any(p => p.Element == gameCommand))
                {
                    trainer.Badges++;
                }
                else
                {
                    foreach (var pokemon in trainer.Pokemon)
                    {
                        pokemon.Health -= 10;
                    }

                    trainer.Pokemon.RemoveAll(p => p.Health <= 0);
                }
            }
        }

        foreach (var trainer in trainers.OrderByDescending(t => t.Badges))
        {
            Console.WriteLine($"{trainer.Name} {trainer.Badges} {trainer.Pokemon.Count}");
        }
    }
}

