using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Models.EntityConfig
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);

            builder.Property(e => e.Balance)
                .IsRequired();

            builder.Property(e => e.BankName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(e => e.SwiftCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Ignore(e => e.PaymentMethodId);
        } 
    }
}
