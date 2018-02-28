using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Commando : SpecialisedSoldier, ICommando
{
    public Commando(string id, string firstName, string lastName, double salary, string corps)
        : base(id, firstName, lastName, salary, corps)
    {
        this.Missions = new List<Mission>();
    }

    public ICollection<Mission> Missions { get; }

    public void CompleteMission(string codeName)
    {
        var currentMission = this.Missions.SingleOrDefault(m => m.CodeName == codeName);
        if (currentMission != null)
        {
            currentMission.State = "Finished";
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Name: {this.FirstName} {this.LastName} Id: {this.Id} Salary: {this.Salary:F2}");
        builder.AppendLine($"Corps: {this.Corps}");
        builder.AppendLine("Missions:");

        foreach (var mission in this.Missions.Where(m => m.State == "inProgress" || m.State == "Finished"))
        {
            builder.AppendLine($"  {mission}");
        }

        return builder.ToString().TrimEnd();
    }
}
