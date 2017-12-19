using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_FootballBetting.Data.Models.Configurations
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(e => e.TownId);

            builder.HasOne(e => e.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(e => e.CountryId);
        }
    }
}