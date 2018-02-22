using System;
using System.Linq;
using System.Collections.Generic;

public class Team
{
    private string name;
    private List<Player> players;

    public Team(string name)
    {
        this.Name = name;
        this.Players = new List<Player>();
    }

    public string Name
    {
        get
        {
            return this.name;
        }

        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("A name should not be empty.");
            }

            this.name = value;
        }
    }

    public List<Player> Players
    {
        get
        {
            return this.players;
        }

        private set
        {
            this.players = value;
        }
    }

    public double Rating => CalculateAverageRating();

    public void AddPlayer(Player player)
    {
        this.Players.Add(player);
    }

    public void RemovePlayer(string playerName)
    {
        if (this.Players.SingleOrDefault(p => p.Name == playerName) is null)
        {
            throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
        }

        var playerToRemove = players.Single(p => p.Name == playerName);

        players.Remove(playerToRemove);
    }

    private int CalculateAverageRating()
    {
        if (players.Count == 0) return 0;

        return (int)Math.Round(players.Select(p => p.SkillLevel).Sum() / this.players.Count);
    }
}