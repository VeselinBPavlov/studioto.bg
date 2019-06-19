namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
