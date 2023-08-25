using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .ToTable("Customers");

            builder
                .HasKey(c => c.CustomerId);

            builder
                .Property(c => c.Email)
                .HasMaxLength(70)
                .IsRequired();

            builder
                .Property(c => c.Cpf)
                .HasMaxLength(11)
                .HasColumnType("char(11)")
                .IsRequired();

            builder
                .Property(c => c.Address)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(c => c.State)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(c => c.City)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(c => c.Cep)
                .HasMaxLength(8)
                .HasColumnType("char(8)")
                .IsRequired();

            builder
                .Property(c => c.PaymentMethod)
                .IsRequired();

            builder
                .Property(c => c.InsertedAt)
                .HasColumnType("datetime2(0)")
                .IsRequired();

            builder
                .Property(c => c.UpdatedAt)
                .HasColumnType("datetime2(0)");
        }
    }
}