using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamBuilder.Data.Models.ModelConfig
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(e => e.InvitationId);

            builder.HasOne(e => e.InvitedUser)
                .WithMany(e => e.Invitations)
                .HasForeignKey(e => e.InvitedUserId);

            builder.HasOne(e => e.Team)
                .WithMany(e => e.Invitations)
                .HasForeignKey(e => e.TeamId);
        }
    }
}
