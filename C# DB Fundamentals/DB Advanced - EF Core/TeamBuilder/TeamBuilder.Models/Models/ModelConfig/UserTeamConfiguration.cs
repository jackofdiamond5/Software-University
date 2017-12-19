using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamBuilder.Data.Models.ModelConfig
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(e => new {e.TeamId, e.UserId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.UserTeams)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Team)
                .WithMany(e => e.UserTeams)
                .HasForeignKey(e => e.TeamId);
        }
    }
}
