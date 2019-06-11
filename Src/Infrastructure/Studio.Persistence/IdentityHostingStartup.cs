using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Studio.Persistence.IdentityHostingStartup))]
namespace Studio.Persistence
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
                services.AddDbContext<StudioDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StudioDBContextConnection")));

                services.AddDefaultIdentity<StudioUser>()
                    .AddEntityFrameworkStores<StudioDBContext>();
            });
        }
    }
}