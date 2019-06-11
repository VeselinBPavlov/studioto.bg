namespace Studio.Persistence
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Domain.Entities;

    public class StudioDbContext : IdentityDbContext<StudioUser>
    {
        public StudioDbContext(DbContextOptions<StudioDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
        }
    }
}
