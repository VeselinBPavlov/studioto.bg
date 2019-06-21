using Microsoft.EntityFrameworkCore;
using Studio.Persistence.Context;
using System;
namespace Studio.Application.Tests
{
    public class TestBase
    {
        public StudioDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<StudioDbContext>();
            
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());            

            var dbContext = new StudioDbContext(builder.Options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
