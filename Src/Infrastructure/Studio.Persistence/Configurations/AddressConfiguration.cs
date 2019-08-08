namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.OwnsOne(a => a.AddressFormat); 

            builder.Property(a => a.Longitude)
                   .HasColumnType("decimal(16, 6)");                              

            builder.Property(a => a.Latitude)
                   .HasColumnType("decimal(16, 6)"); 

            builder.HasOne(a => a.City)
                   .WithMany(c => c.Addresses)
                   .HasForeignKey(a => a.CityId);
        }
    }
}
