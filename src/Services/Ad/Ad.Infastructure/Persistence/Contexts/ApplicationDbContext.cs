using Ad.Domain.Models;
using Ad.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Reflection;

namespace Ad.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<AdModel> Ads { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<UserInfo> UserInfo { get; set; } = null!;

    static ApplicationDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<States>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Categories>();
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresEnum<States>();
        builder.HasPostgresEnum<Categories>();

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
        foreach (var entry in ChangeTracker.Entries<AdModel>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTimeOffset.Now;
            }
        }
    }
}