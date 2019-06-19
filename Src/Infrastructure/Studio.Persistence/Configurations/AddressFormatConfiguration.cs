namespace Studio.Persistence.Configurations
{
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AddressFormatConfiguration : IEntityTypeConfiguration<AddressFormat>
    {
        public void Configure(EntityTypeBuilder<AddressFormat> builder)
        {
            builder.Property(a => a.Street)
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode();

            builder.Property(a => a.Number)
                   .HasMaxLength(10)
                   .IsRequired()
                   .IsUnicode();

            builder.Property(a => a.Building)
                   .HasMaxLength(10)
                   .IsUnicode();

            builder.Property(a => a.Entrance)
                   .HasMaxLength(10)
                   .IsUnicode();

            builder.Property(a => a.Floor)
                   .HasMaxLength(10)
                   .IsUnicode();

            builder.Property(a => a.Apartment)
                   .HasMaxLength(10)
                   .IsUnicode();

            builder.Property(a => a.District)
                   .HasMaxLength(100)
                   .IsUnicode();
        }
    }
}
