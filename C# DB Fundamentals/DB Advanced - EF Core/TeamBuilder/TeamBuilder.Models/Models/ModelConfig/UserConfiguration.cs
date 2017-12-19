using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamBuilder.Data.Models.ModelConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.HasIndex(e => e.Username).IsUnique();
            builder.Property(e => e.Username)
                .IsRequired();

            builder.Property(e => e.FirstName)
                .IsRequired(false);

            builder.Property(e => e.LastName)
                .IsRequired(false);

            builder.Property(e => e.Password)
                .IsRequired(); // CHECK CONSTRAINT

            builder.Property(e => e.Age)
                .IsRequired(false);

            builder.Property(e => e.IsDeleted)
                .IsRequired(false);
        }
    }
}
