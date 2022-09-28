﻿using Ad.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ad.Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

        builder.Property(a => a.CityId).IsRequired();

        builder.Property(a => a.CountryId).IsRequired();

        builder.HasAlternateKey(a => new { a.CountryId, a.CityId });
    }
}