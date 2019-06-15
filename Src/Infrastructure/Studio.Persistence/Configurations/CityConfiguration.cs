﻿namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode();

            builder.HasOne(ci => ci.Country)
                   .WithMany(co => co.Cities)
                   .HasForeignKey(ci => ci.CountryId);                   
        }
    }
}
