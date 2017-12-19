using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Models.EntityConfig
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new
            {
                e.UserId,
                e.CreditCardId,
                e.BankAccountId
            }).IsUnique();

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.BankAccountId)
                .IsRequired(false);

            builder.Property(e => e.CreditCardId)
                .IsRequired(false);

            builder.HasOne(e => e.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.BankAccount)
                .WithOne(ba => ba.PaymentMethod)
                .HasForeignKey<BankAccount>(e => e.BankAccountId);

            builder.HasOne(e => e.CreditCard)
                .WithOne(cc => cc.PaymentMethod)
                .HasForeignKey<CreditCard>(e => e.CreditCardId);
        }
    }
}