using Bag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bag.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

        builder.Property(m => m.UserName).IsRequired().HasMaxLength(30);
        builder.HasIndex(m => m.UserName).IsUnique();

        builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(m => m.PhoneNumber).IsUnique();
    }
}