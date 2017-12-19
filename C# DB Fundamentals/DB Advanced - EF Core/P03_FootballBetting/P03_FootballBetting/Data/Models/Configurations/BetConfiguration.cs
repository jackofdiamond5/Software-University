using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_FootballBetting.Data.Models.Configurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(e => e.BetId);

            builder.Property(e => e.Prediction)
                .IsRequired(true);

            builder.HasOne(e => e.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(e => e.GameId);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(e => e.UserId);
        }
    }
}