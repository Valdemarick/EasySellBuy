using Ad.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ad.Infrastructure.Persistence.Configurations;

public class UserInfoconfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(30);
        builder.HasIndex(u => u.UserName).IsUnique();

        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
    }
}