using System;
using System.Collections.Generic;

namespace TeamBuilder.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.TeamEvents = new List<TeamEvent>();
        }

        public int EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<TeamEvent> TeamEvents { get; set; }
    }
}
