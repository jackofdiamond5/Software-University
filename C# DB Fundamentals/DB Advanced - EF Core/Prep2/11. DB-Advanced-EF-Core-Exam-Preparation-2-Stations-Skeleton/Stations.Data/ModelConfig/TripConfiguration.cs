using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.ModelConfig
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.OriginStation)
                .WithMany(t => t.TripsFrom)
                .HasForeignKey(e => e.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DestinationStation)
                .WithMany(t => t.TripsTo)
                .HasForeignKey(e => e.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Train)
                .WithMany(tr => tr.Trips)
                .HasForeignKey(e => e.TrainId);

            builder.Property(e => e.Status)
                .HasDefaultValue(TripStatus.OnTime);
        }
    }
}
