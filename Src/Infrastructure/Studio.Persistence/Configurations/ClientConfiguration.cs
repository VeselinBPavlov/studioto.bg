namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.HasOne(c => c.Address)
                   .WithOne(a => a.Client)
                   .HasForeignKey<Address>(c => c.ClientId);
        }
    }
}
