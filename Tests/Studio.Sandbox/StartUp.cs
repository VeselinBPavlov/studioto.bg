namespace Studio.Sandbox
{
    using System;
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Core;
    using Core.Contracts;
    using Studio.Persistence.Context;
    using MediatR;
    using System.Reflection;
    using Studio.Application.Infrastructure.Logger;
    using Studio.Application.Industries.Commands.Create;

    public class StartUp
    {
        public static void Main()
        {
            var service = ConfigureServices();
            Engine engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<StudioDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("StudioDBConnectionHome")));

            services.AddMediatR(typeof(CreateIndustryCommandHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<ICommandInterpreter, CommandInterpreter>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}