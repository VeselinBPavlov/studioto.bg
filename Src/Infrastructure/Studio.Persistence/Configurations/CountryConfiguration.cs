namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Name)                   
                   .IsUnique();

            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsRequired()
                   .IsUnicode();
        }
    }
}
