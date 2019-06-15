namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode();
        }
    }
}
