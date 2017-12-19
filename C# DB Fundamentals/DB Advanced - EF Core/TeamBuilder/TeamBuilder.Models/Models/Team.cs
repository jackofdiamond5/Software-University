using System.Collections.Generic;

namespace TeamBuilder.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.Invitations = new List<Invitation>();
            this.UserTeams = new List<UserTeam>();
            this.TeamEvents = new List<TeamEvent>();
        }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Acronym { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<TeamEvent> TeamEvents { get; set; }
    }
}
