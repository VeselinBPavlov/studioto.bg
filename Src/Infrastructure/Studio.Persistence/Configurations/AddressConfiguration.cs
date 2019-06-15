namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Floor)
                   .HasMaxLength(20)
                   .IsUnicode();

            builder.Property(a => a.Number)
                   .HasMaxLength(20)
                   .IsRequired()
                   .IsUnicode();

            builder.Property(a => a.Street)
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode();

            builder.Property(a => a.District)
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.Property(a => a.PostalCode)
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.HasOne(a => a.City)
                   .WithMany(c => c.Addresses)
                   .HasForeignKey(a => a.CityId);
        }
    }
}
