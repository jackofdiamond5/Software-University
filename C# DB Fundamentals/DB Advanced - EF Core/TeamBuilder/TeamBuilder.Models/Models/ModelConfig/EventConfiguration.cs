using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamBuilder.Data.Models.ModelConfig
{
   public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);

            builder.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(250);

            // CHECK ON END DATE

            builder.HasOne(e => e.Creator)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.CreatorId);
        }
    }
}
