using Microsoft.EntityFrameworkCore;

namespace Studio.Persistence.Infrastructure
{
    public class StudioDbContextFactory : DesignTimeDbContextFactoryBase<StudioDbContext>
    {
        protected override StudioDbContext CreateNewInstance(DbContextOptions<StudioDbContext> options)
        {
            return new StudioDbContext(options);
        }
    }
}
