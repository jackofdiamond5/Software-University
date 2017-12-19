
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TeamBuilder.Data.Models
{
    public class User
    {
        public User()
        {
            this.Events = new List<Event>();
            this.Invitations = new List<Invitation>();
            this.UserTeams = new List<UserTeam>();
        }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }


    }
}
