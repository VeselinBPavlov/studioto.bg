namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClientFormConfiguration : IEntityTypeConfiguration<ContactForm>
    {
        public void Configure(EntityTypeBuilder<ContactForm> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();
            
            builder.Property(c => c.LastName)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();
            
            builder.Property(c => c.Email)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(c => c.Topic)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(c => c.Message)
                   .IsUnicode()
                   .IsRequired();
        }
    }
}