using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var teams = new List<Team>();

        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            var args = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var command = args[0];

            if (command == "Team")
            {
                teams.Add(new Team(args[1]));
                continue;
            }

            var team = teams.FirstOrDefault(t => t.Name == args[1]);
            if (team is null)
            {
                Console.WriteLine($"Team {args[1]} does not exist.");
                continue;
            }

            try
            {
                switch (command)
                {
                    case "Add":
                        AddPlayerToTeam(args, teams);
                        break;

                    case "Remove":
                        RemovePlayerFromTeam(args, teams);
                        break;

                    case "Rating":
                        GetATeamsRating(args, teams);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private static void CreateTeam(List<Team> teams, string[] args)
    {
        var teamName = args[1];
        teams.Add(new Team(teamName));
    }

    private static void GetATeamsRating(string[] args, List<Team> teams)
    {
        var teamName = args[1];

        Console.WriteLine($"{teamName} - {teams.SingleOrDefault(t => t.Name == teamName).Rating}");
    }

    private static void RemovePlayerFromTeam(string[] args, List<Team> teams)
    {
        var teamName = args[1];
        var playerToRemove = args[2];

        teams.SingleOrDefault(t => t.Name == teamName).RemovePlayer(playerToRemove);
    }

    private static void AddPlayerToTeam(string[] args, List<Team> teams)
    {
        var playerToAdd = args[2];
        var teamName = args[1];

        var player = new Player(playerToAdd, args.Skip(3).Select(int.Parse).ToArray());

        teams.SingleOrDefault(t => t.Name == teamName).AddPlayer(player);
    }
}

