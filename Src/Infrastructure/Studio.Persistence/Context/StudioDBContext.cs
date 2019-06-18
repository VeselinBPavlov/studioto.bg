namespace Studio.Persistence.Context
{
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Linq;
    using Studio.Domain.Interfaces;
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;  
    using Microsoft.EntityFrameworkCore.Migrations;

    using Infrastructure;

    using Domain.Entities;
    using Application.Interfaces.Persistence;

    public class StudioDbContext : IdentityDbContext<StudioUser, StudioRole, string>, IStudioDbContext
    {
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeService> EmployeeServices { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationMapData> LocationsMapData { get; set; }

        public DbSet<LocationService> LocationServices { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<StudioRole> StudioRoles { get; set; }

        public DbSet<StudioUser> StudioUsers { get; set; }

        public DbSet<Industry> Industries { get; set;}

        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(StudioDbContext).GetMethod(nameof(SetIsDeletedQueryFilter), BindingFlags.NonPublic | BindingFlags.Static);

        public StudioDbContext(DbContextOptions<StudioDbContext> options)
            : base(options)
        {
        }               

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(StudioDbContext).Assembly);            
            
            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomSqlServerMigrationsSqlGenerator>();
        }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
