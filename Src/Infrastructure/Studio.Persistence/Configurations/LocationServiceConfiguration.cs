namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LocationServiceConfiguration : IEntityTypeConfiguration<LocationService>
    {
        public void Configure(EntityTypeBuilder<LocationService> builder)
        {
            builder.HasKey(ls => new { ls.LocationId, ls.ServiceId });

            builder.Property(ls => ls.IsActive)
                   .IsRequired();

            builder.Property(ls => ls.Price)
                   .IsRequired();
        }
    }
}
