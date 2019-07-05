namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Studio.Domain.Entities;

    public class AdministrationConfiguration : IEntityTypeConfiguration<Administration>
    {
        public void Configure(EntityTypeBuilder<Administration> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(a => a.Value)
                   .HasMaxLength(10)
                   .IsUnicode()
                   .IsRequired();
        }
    }
}