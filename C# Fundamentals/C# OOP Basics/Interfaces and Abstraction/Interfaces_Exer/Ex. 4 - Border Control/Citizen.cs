public class Citizen : ICitizen, IAlive
{
    public Citizen(string citizenName, int citizenAge, string citizenId, string birthdate)
    {
        this.Name = citizenName;
        this.Age = citizenAge;
        this.Id = citizenId;
        this.Birthdate = birthdate;
    }

    public string Id { get; }

    public string Name { get; }

    public int Age { get; }

    public string Birthdate { get; }

    public override string ToString()
    {
        return this.Birthdate;
    }
}