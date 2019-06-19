namespace Studio.Persistence.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Persistence.Context;

    public class StudioDbContextFactory : DesignTimeDbContextFactoryBase<StudioDbContext>
    {
        protected override StudioDbContext CreateNewInstance(DbContextOptions<StudioDbContext> options)
        {
            return new StudioDbContext(options);
        }
    }
}
