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

            builder.Property(a => a.Comment)
                   .HasMaxLength(450)
                   .IsUnicode();

            builder.Property(a => a.TimeBlockHelper)
                   .HasMaxLength(20)
                   .IsRequired()
                   .IsUnicode();

            builder.HasOne(a => a.Employee)
                   .WithMany(e => e.Appointments)
                   .HasForeignKey(a => a.EmployeeId);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Appointments)
                   .HasForeignKey(a => a.UserId)
                   .IsRequired(false);
        }
    }
}
