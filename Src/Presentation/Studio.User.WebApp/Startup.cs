namespace Studio.User.WebApp
{
    using System.Reflection;

    using Application.Infrastructure.AutoMapper;
    using Application.Infrastructure.Logger;
    using Application.Interfaces.Infrastructure;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence.Context;
    using Studio.Application.Countries.Queries.GetAllCountries;
    using Studio.Application.Interfaces.Persistence;
    using FluentValidation.AspNetCore;
    using Studio.Application.Clients.Commands.Create;
    using Studio.User.WebApp.FIlters;
    using Studio.Application.Infrastructure.SendGrid;
    using Studio.Application.Infrastructure.Validatior;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // MediatR
            services.AddMediatR(typeof(GetAllCountriesListQueryHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Application services
            services.AddTransient<ISender, SendGridEmailSender>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<StudioDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("StudioDBConnectionHome")));
            
            services.AddScoped<IStudioDbContext, StudioDbContext>();

            services.AddDefaultIdentity<StudioUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddDefaultUI(UIFramework.Bootstrap3)
                .AddRoles<StudioRole>()
                .AddEntityFrameworkStores<StudioDbContext>();

            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateClientCommandValidator>();
                    fv.ConfigureClientsideValidation(clientSideValidation =>
                    {
                        clientSideValidation.Add(typeof(RequiredIfValidator), (context, rule, validator) => new RequiredIfClientValidator(rule, validator));
                    });
                }) 
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

             services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                using(var context = serviceScope.ServiceProvider.GetRequiredService<StudioDbContext>())
                {
                    StudioInitializer.Initialize(context);
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseStatusCodePagesWithRedirects("~/Home/Error/{0}");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Administrator",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
