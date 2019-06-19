namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LocationMapDataConfiguration : IEntityTypeConfiguration<LocationMapData>
    {
        public void Configure(EntityTypeBuilder<LocationMapData> builder)
        {
            builder.HasKey(lmd => lmd.Id);

            builder.HasOne(lmd => lmd.Location)
                   .WithOne(l => l.LocationMapData)
                   .HasForeignKey<LocationMapData>(lmd => lmd.LocationId);
        }
    }
}
