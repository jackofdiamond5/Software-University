using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.ModelConfig
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.CustomerCard)
                .WithMany(cc => cc.BoughtTickets)
                .HasForeignKey(e => e.CustomerCardId);

            builder.HasOne(e => e.Trip)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(e => e.TripId);
        }
    }
}
