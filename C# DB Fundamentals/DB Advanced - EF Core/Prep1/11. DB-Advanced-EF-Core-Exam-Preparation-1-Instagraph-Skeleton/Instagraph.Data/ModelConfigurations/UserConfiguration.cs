using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.ModelConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(30);
            builder.HasIndex(e => e.Username)
                .IsUnique();

            builder.HasOne(e => e.ProfilePicture)
                .WithMany(pp => pp.Users)
                .HasForeignKey(e => e.ProfilePictureId);
        }
    }
}
