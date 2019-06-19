namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                   .HasMaxLength(250)
                   .IsUnicode()
                   .IsRequired();

            builder.HasOne(a => a.Location)
                   .WithMany(l => l.Appointments)
                   .HasForeignKey(a => a.LocationId);

            builder.HasOne(a => a.Employee)
                   .WithMany(e => e.Appointments)
                   .HasForeignKey(a => a.EmployeeId);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Appointments)
                   .HasForeignKey(a => a.UserId);
        }
    }
}
