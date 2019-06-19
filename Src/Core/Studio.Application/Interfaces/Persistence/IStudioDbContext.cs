namespace Studio.Application.Interfaces.Persistence
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IStudioDbContext
    {
        DbSet<Setting> Settings { get; set; }

        DbSet<Appointment> Appointments { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<Employee> Employees { get; set; }

        DbSet<EmployeeService> EmployeeServices { get; set; }

        DbSet<Location> Locations { get; set; }

        DbSet<LocationMapData> LocationsMapData { get; set; }

        DbSet<LocationService> LocationServices { get; set; }

        DbSet<Service> Services { get; set; }

        DbSet<StudioRole> StudioRoles { get; set; }

        DbSet<StudioUser> StudioUsers { get; set; }
    }
}
