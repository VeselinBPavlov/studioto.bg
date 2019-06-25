namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CompanyName)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.OwnsOne(c => c.Manager);   

            builder.Property(c => c.VatNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(); 

            builder.Property(c => c.Phone)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired();
        }
    }
}
