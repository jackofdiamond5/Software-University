using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Models.EntityConfig
{
    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(e => e.CreditCardId);


            builder.Property(e => e.MoneyOwed)
                .IsRequired();

            builder.Property(e => e.ExpirationDate)
                .IsRequired();


            builder.Ignore(e => e.PaymentMethodId);

            builder.Ignore(e => e.LimitLeft);
        }
    }
}
