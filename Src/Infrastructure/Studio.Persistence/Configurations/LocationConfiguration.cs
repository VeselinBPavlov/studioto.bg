
using Microsoft.EntityFrameworkCore;
namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.HasOne(l => l.LocationMapData)
                   .WithOne(lmd => lmd.Location)
                   .HasForeignKey<Location>(l => l.LocationMapDataId);
        }
    }
}
