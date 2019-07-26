// namespace Studio.Presentation.Tests.Common
// {
//     using Microsoft.Extensions.DependencyInjection;
//     using Studio.User.WebApp;
//     using Microsoft.Extensions.Configuration;
//     using Studio.Persistence.Context;
//     using Microsoft.EntityFrameworkCore;

//     public class TestStartup : Startup
//     {
//         public TestStartup(IConfiguration configuration) 
//             : base(configuration)
//         {
//         }

//         public override void SetUpDataBase(IServiceCollection services)
// 	    {
//             services
//                 .AddEntityFrameworkInMemoryDatabase()
//                 .AddDbContext<StudioDbContext>(options => 
//                options.UseInMemoryDatabase("MyTestDatabase"));

//                 SeedData(services);
//         }
//         public void ConfigureTestServices(IServiceCollection services)
//         {
//             base.ConfigureServices(services);
//         }

//         private void SeedData(IServiceCollection services)
//         {

//             using(var serviceScope = services.BuildServiceProvider().CreateScope())
//             {
//                 using(var context = serviceScope.ServiceProvider.GetRequiredService<StudioDbContext>())
//                 {
//                     StudioInitializer.Initialize(context);
//                 }
//             }
//         }
//     }
// }
