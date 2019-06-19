namespace Studio.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudioRoleConfiguration : IEntityTypeConfiguration<StudioRole>
    {
        public void Configure(EntityTypeBuilder<StudioRole> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
