using Bag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bag.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Seller> Sellers { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        SetDateTime();

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDateTime();

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void SetDateTime()
    {
        foreach (var entry in ChangeTracker.Entries<Order>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTimeOffset.Now;
            }
        }
    }
}