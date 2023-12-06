using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Infrastructure.DataAccess;

public class VedaDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VedaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers = null!;
}