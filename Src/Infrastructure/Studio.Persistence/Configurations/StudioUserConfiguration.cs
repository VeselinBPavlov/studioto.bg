namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudioUserConfiguration : IEntityTypeConfiguration<StudioUser>
    {
        public void Configure(EntityTypeBuilder<StudioUser> builder)
        {
            builder.HasMany(e => e.Claims)
                   .WithOne()
                   .HasForeignKey(e => e.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Logins)
                   .WithOne()
                   .HasForeignKey(e => e.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Roles)
                   .WithOne()
                   .HasForeignKey(e => e.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.FirstName)
                   .HasMaxLength(50)
                   .IsUnicode();

            builder.Property(e => e.LastName)
                   .HasMaxLength(50)
                   .IsUnicode();
        }
    }
}
