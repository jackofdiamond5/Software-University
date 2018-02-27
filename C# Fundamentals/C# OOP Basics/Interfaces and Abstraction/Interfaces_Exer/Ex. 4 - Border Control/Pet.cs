class Pet : IAlive
{
    public Pet(string name, string birthdate)
    {
        this.Name = name;
        this.Birthdate = birthdate;
    }

    public string Name { get; }
    
    public string Birthdate { get; }

    public override string ToString()
    {
        return this.Birthdate;
    }
}

