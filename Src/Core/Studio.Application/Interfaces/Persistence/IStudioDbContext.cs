﻿namespace Studio.Application.Interfaces.Persistence
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IStudioDbContext
    {
        DbSet<Appointment> Appointments { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<Employee> Employees { get; set; }

        DbSet<EmployeeService> EmployeeServices { get; set; }

        DbSet<Location> Locations { get; set; }
        
        DbSet<LocationIndustry> LocationIndustries { get; set; }

        DbSet<Service> Services { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<City> Cities { get; set; }

        DbSet<Address> Addresses { get; set; }

        DbSet<StudioRole> StudioRoles { get; set; }

        DbSet<StudioUser> StudioUsers { get; set; }

        DbSet<Industry> Industries { get; set; }

        DbSet<ContactForm> ContactForms { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
