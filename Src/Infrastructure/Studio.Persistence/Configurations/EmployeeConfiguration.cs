namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(c => c.LastName)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.HasOne(e => e.Location)
                   .WithMany(l => l.Employees)
                   .HasForeignKey(e => e.LocationId);
        }
    }
}
