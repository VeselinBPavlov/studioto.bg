namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LocationServiceConfiguration : IEntityTypeConfiguration<LocationIndustry>
    {
        public void Configure(EntityTypeBuilder<LocationIndustry> builder)
        {
            builder.HasKey(ls => new { ls.LocationId, ls.IndustryId });

            builder.Property(ls => ls.IsActive)
                   .IsRequired();

            builder.Property(ls => ls.Description)
                   .IsUnicode()
                   .IsRequired();
        }
    }
}
