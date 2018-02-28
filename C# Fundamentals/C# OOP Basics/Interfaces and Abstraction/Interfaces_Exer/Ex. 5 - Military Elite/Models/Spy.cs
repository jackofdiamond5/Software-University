using System.Text;

public class Spy : Soldier, ISpy
{
    public Spy(string id, string firstName, string lastName, int codeNumber)
        : base(id, firstName, lastName)
    {
        this.CodeNumber = codeNumber;
    }

    public int CodeNumber { get; }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder
            .AppendLine($"Name: {this.FirstName} {this.LastName} Id: {this.Id}")
            .AppendLine($"Code Number: {this.CodeNumber}");

        return builder.ToString().TrimEnd();
    }
}
