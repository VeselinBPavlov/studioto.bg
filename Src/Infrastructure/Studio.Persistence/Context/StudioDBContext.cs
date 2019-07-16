namespace Studio.Persistence.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces.Persistence;
    using Domain.Entities;
    using Domain.Interfaces;
    using Infrastructure;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Migrations;

    public class StudioDbContext : IdentityDbContext<StudioUser, StudioRole, string>, IStudioDbContext
    {
        public StudioDbContext(DbContextOptions<StudioDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeService> EmployeeServices { get; set; }

        public DbSet<Location> Locations { get; set; }
        
        public DbSet<LocationIndustry> LocationIndustries { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<StudioRole> StudioRoles { get; set; }

        public DbSet<StudioUser> StudioUsers { get; set; }

        public DbSet<Industry> Industries { get; set; }

        public DbSet<ContactForm> ContactForms { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(StudioDbContext).Assembly);            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomSqlServerMigrationsSqlGenerator>();
        }        
    }
}
