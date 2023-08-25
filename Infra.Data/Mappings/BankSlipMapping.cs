using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class BankSlipMapping : IEntityTypeConfiguration<BankSlip>
    {
        public void Configure(EntityTypeBuilder<BankSlip> builder)
        {
            builder
                .ToTable("BankSlips");

            builder
                .HasKey(c => c.BankSlipId);

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