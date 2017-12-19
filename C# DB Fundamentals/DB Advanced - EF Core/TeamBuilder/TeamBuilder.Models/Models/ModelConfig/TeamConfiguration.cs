using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamBuilder.Data.Models.ModelConfig
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.TeamId);

            builder.Property(e => e.Name)
                .HasMaxLength(25)
                .IsRequired();
            builder.HasIndex(e => e.Name).IsUnique();

            builder.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(32);

            builder.Property(e => e.Acronym)
                .IsRequired()
                .HasColumnType("CHAR(10)");

            //builder.HasOne(e => e.Creator);
        }
    }
}
