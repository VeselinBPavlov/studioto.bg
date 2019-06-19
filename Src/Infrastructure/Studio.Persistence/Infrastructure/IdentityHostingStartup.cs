using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Studio.Persistence.Infrastructure.IdentityHostingStartup))]

namespace Studio.Persistence.Infrastructure
{
    using Context;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => 
            {
                services.AddDbContext<StudioDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StudioDBConnectionHome")));

                services.AddDefaultIdentity<StudioUser>()
                    .AddEntityFrameworkStores<StudioDbContext>();
            });
        }
    }
}