using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamBuilder.Data.Models.ModelConfig
{
    public class TeamEventConfiguration : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.HasKey(e => new { e.EventId, e.TeamId });

            builder.HasOne(e => e.Team)
                .WithMany(e => e.TeamEvents)
                .HasForeignKey(e => e.TeamId);

            builder.HasOne(e => e.Event)
                .WithMany(e => e.TeamEvents)
                .HasForeignKey(e => e.EventId);
        }
    }
}
