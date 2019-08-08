namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeServiceConfiguration : IEntityTypeConfiguration<EmployeeService>
    {
        public void Configure(EntityTypeBuilder<EmployeeService> builder)
        {
            builder.HasKey(es => new { es.EmployeeId, es.ServiceId });

            builder.Property(a => a.Price)
                   .HasColumnType("decimal(16, 2)"); 

            builder.Property(es => es.DurationInMinutes)
                   .IsRequired();
        }
    }
}
