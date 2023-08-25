using Domain.Models;
using Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context;

public class ConfigContext : DbContext
{
    public ConfigContext(DbContextOptions<ConfigContext> option) : base(option)
    {
    }

    public DbSet<Customer>? Customer { get; set; }
    public DbSet<CreditCard>? CreditCard { get; set; }
    public DbSet<BankSlip>? BankSlip { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerMapping());
        modelBuilder.ApplyConfiguration(new CreditCardMapping());
        modelBuilder.ApplyConfiguration(new BankSlipMapping());

        base.OnModelCreating(modelBuilder);
    }
}