using System;

public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
{
    private string corps;

    protected SpecialisedSoldier(string id, string firstName, string lastName, double salary, string corps)
        : base(id, firstName, lastName, salary)
    {
        this.Corps = corps;
    }

    public string Corps
    {
        get => this.corps;
        private set
        {
            if (value.Trim() != "Airforces" && value.Trim() != "Marines")
            {
                throw new ArgumentException();
            }

            this.corps = value;
        }
    }
}
