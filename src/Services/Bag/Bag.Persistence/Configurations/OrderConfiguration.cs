using Bag.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bag.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

        builder.Property(m => m.SellerId).IsRequired();

        builder.Property(m => m.CustomerId).IsRequired();

        builder.Property(m => m.TotalAmount).IsRequired().HasColumnType("money");

        builder.Property(m => m.CreatedOn).IsRequired();
    }
}