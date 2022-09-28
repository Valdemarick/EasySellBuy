using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ad.Infrastructure.Persistence.Configurations;

public class AdConfigurations : IEntityTypeConfiguration<AdModel>
{
    public void Configure(EntityTypeBuilder<AdModel> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

        builder.Property(a => a.Title).IsRequired().HasMaxLength(50);

        builder.Property(a => a.Description).IsRequired().HasColumnType("text");

        builder.Property(a => a.CreatedOn).IsRequired();

        builder.Property(a => a.Price).IsRequired().HasColumnType("money");

        builder.Property(a => a.UserInfoId).IsRequired();

        builder.Property(a => a.AddressId).IsRequired();

        builder.Property(a => a.State).IsRequired();

        builder.Property(a => a.Category).IsRequired();
    }
}