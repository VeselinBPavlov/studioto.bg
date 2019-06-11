using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Studio.Persistence.Infrastructure.IdentityHostingStartup))]
namespace Studio.Persistence.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Domain.Entities;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StudioDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StudioDBConnection")));

                services.AddDefaultIdentity<StudioUser>()
                    .AddEntityFrameworkStores<StudioDbContext>();
            });
        }
    }
}