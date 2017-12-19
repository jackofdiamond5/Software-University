using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.ModelConfig
{
    public class CustomerCardConfiguration : IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Type).HasDefaultValue(CardType.Normal);
        }
    }
}
