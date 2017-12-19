using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_FootballBetting.Data.Models.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.TeamId);

            builder.HasOne(e => e.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(e => e.PrimaryKitColorId);

            builder.HasOne(e => e.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(e => e.SecondaryKitColorId);

            builder.HasOne(e => e.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(e => e.TownId);
        }
    }
}