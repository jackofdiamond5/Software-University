using System;
using System.Linq;

public class Player
{
    private string[] StatNames = { "Endurance", "Sprint", "Dribble", "Passing", "Shooting" };
    private const int LowerBound = 0;
    private const int UpperBound = 100;

    private string name;
    private int[] stats;
    private const int StatsCount = 5;

    public Player(string name, int[] stats)
    {
        this.Name = name;
        this.Stats = stats;
    }

    public string Name
    {
        get => this.name;

        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("A name should not be empty.");
            }
            
            this.name = value;
        }
    }

    public int[] Stats
    {
        get => stats;

        private set
        {
            this.stats = new int[5];

            for (var i = 0; i < this.stats.Length; i++)
            {
                if (value[i] < 0 || value[i] > 100)
                {
                    throw new ArgumentException($"{StatNames[i]} should be between {LowerBound} and {UpperBound}.");
                }

                this.stats[i] = value[i];
            }
        }
    }

    public double SkillLevel => Stats.Average();
}

