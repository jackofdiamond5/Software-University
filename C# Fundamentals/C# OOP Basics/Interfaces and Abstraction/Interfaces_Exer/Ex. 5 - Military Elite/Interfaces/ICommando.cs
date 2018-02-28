using System.Collections.Generic;

public interface ICommando : ISpecialisedSoldier
{
    ICollection<Mission> Missions { get; }
}
