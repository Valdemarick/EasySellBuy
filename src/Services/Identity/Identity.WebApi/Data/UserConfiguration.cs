using Identity.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Converters;

namespace Identity.WebApi.Data;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);

        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);

        builder.Property(u => u.DateOfBirth).IsRequired().HasConversion<DateOnlyConverter>().HasColumnType("date");
        builder.HasCheckConstraint("DateOfBirth", "DATEDIFF(year, DateOfBirth, CONVERT(date, GETDATE())) >= 14");
    }
}