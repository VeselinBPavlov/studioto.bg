namespace Studio.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class StudioRoleConfiguration : IEntityTypeConfiguration<StudioRole>
    {
        public void Configure(EntityTypeBuilder<StudioRole> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
