namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Studio.Domain.ValueObjects;

    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Manager> builder)
        {
            builder.Property(m => m.FirstName)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(m => m.LastName)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();
        }
    }
}