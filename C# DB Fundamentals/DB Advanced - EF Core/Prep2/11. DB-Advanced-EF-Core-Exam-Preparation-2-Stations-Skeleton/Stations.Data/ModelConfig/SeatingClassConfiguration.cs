using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.ModelConfig
{
    public class SeatingClassConfiguration : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Abbreviation);

            builder.HasAlternateKey(e => e.Name);
        }
    }
}
