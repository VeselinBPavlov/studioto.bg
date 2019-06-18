namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
    {
        public void Configure(EntityTypeBuilder<Industry> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                   .HasMaxLength(100)
                   .IsRequired()
                   .IsUnicode();
        }
    }
}
