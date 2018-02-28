using System.Collections.Generic;

public interface IEngineer : ISpecialisedSoldier
{
    ICollection<Repair> Repairs { get; }
}
