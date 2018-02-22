using System;
using System.Linq;

class Dough
{
    private const int BaseCalories = 2;

    private static readonly string[] FlourTypes = { "white", "wholegrain" };
    private static readonly string[] Techniques = { "crispy", "chewy", "homemade" };
    private const double MinWeight = 1;
    private const double MaxWeight = 200;

    private const double WhiteFlourVal = 1.5;
    private const double WholeGrainFlourVal = 1.0;

    private const double CrispyTechniqueVal = 0.9;
    private const double ChewyTechniqueVal = 1.1;
    private const double HomemadeTechniqueVal = 1.0;

    private string flourType;
    private string bakingTechnique;
    private double weight;


    public Dough(string flourType, string bakingTechnique, double weight)
    {
        this.FlourType = flourType;
        this.BakingTechnique = bakingTechnique;
        this.Weight = weight;
    }


    public string FlourType
    {
        get => this.flourType;
        private set
        {
            if (!FlourTypes.Contains(value.ToLower()))
            {
                throw new ArgumentException("Invalid type of dough.");
            }

            this.flourType = value;
        }
    }

    public string BakingTechnique
    {
        get => this.bakingTechnique;
        private set
        {
            if (!Techniques.Contains(value.ToLower()))
            {
                throw new ArgumentException("Invalid type of dough.");
            }

            this.bakingTechnique = value;
        }
    }

    public double Weight
    {
        get => this.weight;
        private set
        {
            if (value < MinWeight || value > MaxWeight)
            {
                throw new ArgumentException($"Dough weight should be in the range [{MinWeight}..{MaxWeight}].");
            }

            this.weight = value;
        }
    }

    public double Calories => this.CalculateCalories();

    private double CalculateCalories()
    {
        var baseCaloriesPerGram = BaseCalories * this.weight;

        var flour = this.flourType.ToLower().Equals("white") ? WhiteFlourVal : WholeGrainFlourVal;

        var technique = this.bakingTechnique.ToLower().Equals("crispy")
            ? CrispyTechniqueVal
            : this.bakingTechnique.ToLower().Equals("chewy")
                ? ChewyTechniqueVal
                : HomemadeTechniqueVal;

        return baseCaloriesPerGram * flour * technique;
    }
}