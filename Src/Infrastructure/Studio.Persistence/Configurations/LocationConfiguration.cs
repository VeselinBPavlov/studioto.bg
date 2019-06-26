namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();
            
            builder.HasOne(l => l.Address)
                   .WithOne(a => a.Location)
                   .HasForeignKey<Location>(l => l.AddressId);

            builder.HasOne(l => l.Client)
                   .WithMany(c => c.Locations)
                   .HasForeignKey(l => l.ClientId);
        }
    }
}
