namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Studio.Persistence.Context;

    public class StudioDBContextFactory
    {
        public static StudioDbContext Create()
        {
            var options = new DbContextOptionsBuilder<StudioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new StudioDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(StudioDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}