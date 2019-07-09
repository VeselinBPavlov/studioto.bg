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

            builder.Property(c => c.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c => c.Slogan)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(c => c.Description)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(c => c.StartHour)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c => c.EndHour)
                   .HasMaxLength(20)
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
