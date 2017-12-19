using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.ModelConfig
{
    public class TrainSeatConfiguration : IEntityTypeConfiguration<TrainSeat>
    {
        public void Configure(EntityTypeBuilder<TrainSeat> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.SeatingClass)
                .WithMany(sc => sc.TrainSeats)
                .HasForeignKey(e => e.SeatingClassId);

            builder.HasOne(e => e.Train)
                .WithMany(tr => tr.TrainSeats)
                .HasForeignKey(e => e.TrainId);
        }
    }
}
