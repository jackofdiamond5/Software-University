using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_FootballBetting.Data.Models.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(e => e.PlayerId);

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(e => e.TeamId);

            builder.HasOne(e => e.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(e => e.PlayerId);
        }
    }
}