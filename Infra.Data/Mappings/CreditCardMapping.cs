using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class CreditCardMapping : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder
                .ToTable("CreditCards");

            builder
                .HasKey(c => c.CreditCardId);

            builder
                .Property(c => c.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder
                .Property(c => c.Number)
                .HasMaxLength(16)
                .IsRequired();

            builder
                .Property(c => c.ExpirationDate)
                .HasColumnType("datetime(0)")
                .IsRequired();

            builder
                .Property(c => c.SecurityCode)
                .HasMaxLength(3)
                .HasColumnType("char(3)")
                .IsRequired();

            builder
                .Property(c => c.CustomerId)
                .IsRequired();

            builder
                .Property(c => c.InsertedAt)
                .HasColumnType("datetime(0)")
                .IsRequired();

            builder
                .Property(c => c.UpdatedAt)
                .HasColumnType("datetime(0)");
        }
    }
}