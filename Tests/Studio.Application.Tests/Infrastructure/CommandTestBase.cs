namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Persistence.Context;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Domain.ValueObjects;

    public class CommandTestBase : IDisposable
    {
        protected readonly StudioDbContext context;

        public CommandTestBase()
        {
            this.context = StudioDBContextFactory.Create();
        }

        public void Dispose()
        {
            StudioDBContextFactory.Destroy(this.context);
        }

        public int GetCityId(int? countryId)
        {
            City city = new City { Name = GConst.ValidName };

            if (countryId != null)
            {
                city.CountryId = countryId.Value;
            }

            context.Cities.Add(city);
            this.context.SaveChangesAsync();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            return cityId;
        }

        public int GetAddressId(int? cityId)
        {
            var inputAddressData = new InputAddressData
            {
                Street = GConst.ValidName,
                Number = GConst.ValidAddressNumber
            };

            Address address = new Address { AddressFormat = AddressFormat.For(inputAddressData) };

            if (cityId != null)
            {            
                address = new Address { AddressFormat = AddressFormat.For(inputAddressData), CityId = cityId.Value };
            }

            context.Addresses.Add(address);
            context.SaveChangesAsync();

            var addressId = context.Addresses.SingleOrDefault(x => x.AddressFormat.Street == GConst.ValidName).Id;

            return addressId;
        }

        public int GetClientId()
        {
            var client = new Client { CompanyName = GConst.ValidName };

            context.Clients.Add(client);
            context.SaveChanges();

            var clientId = context.Clients.SingleOrDefault(x => x.CompanyName == GConst.ValidName).Id;

            return clientId;
        }

        public int GetLocationId(int? clientId, int? addressId)
        {
            Location location = new Location 
            { 
                Name = GConst.ValidName,  
                StartHour = GConst.ValidStartHour,
                EndHour = GConst.ValidEndHour
            };

            if (clientId != null)
            {
                location.ClientId = clientId.Value;
            }

            if (addressId != null)
            {
                location.AddressId = addressId.Value;
            }

            context.Locations.Add(location);
            context.SaveChanges();

            var locationId = context.Locations.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            return locationId;
        }

        public int GetCountryId()
        {
            var country = new Country { Name = GConst.ValidName };
            context.Countries.Add(country);
            this.context.SaveChanges();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            return countryId;
        }

        public int GetIndustryId()
        {
            var industry = new Industry { Name = GConst.ValidName };

            context.Industries.Add(industry);
            context.SaveChanges();

            var industryId = context.Industries.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            return industryId;
        }

        public int GetServiceId(int? industryId)
        {
            Service service = new Service { Name = GConst.ValidName };

            if (industryId != null)
            {
                service.IndustryId = industryId.Value;
            }

            context.Services.Add(service);
            context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            return serviceId;
        }

        public int GetEmployeeId(int? locationId)
        {
            var employee = new Employee { FirstName = GConst.ValidName };            

            if (locationId != null) 
            {
                employee.LocationId = locationId.Value;
            }

            context.Employees.Add(employee);
            context.SaveChangesAsync();

            var employeeId = context.Employees.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            return employeeId;
        }

        public string GetUserId()
        {
            var user = new StudioUser { UserName = GConst.ValidName, Email = GConst.ValidEmail };

            context.StudioUsers.Add(user);
            context.SaveChanges();

            var userId = context.StudioUsers.SingleOrDefault(x => x.UserName == GConst.ValidName).Id;

            return userId;
        }

        public int GetAppointmentId(int? serviceId, int? employeeId, string userId)
        {
            Appointment appointment = new Appointment
            {
                FirstName = GConst.ValidName,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
            };
        
            if (serviceId != null) 
            {
                appointment.ServiceId = serviceId.Value;
            }

            if (employeeId != null) 
            {
                appointment.EmployeeId = employeeId.Value;                
            }

            if (userId != null) 
            {
                appointment.UserId = userId;  
            }

            appointment.ReservationTime = DateTime.Parse(appointment.TimeBlockHelper);

            context.Appointments.Add(appointment);
            context.SaveChanges();

            var appointmentId = context.Appointments.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            return appointmentId;
        }

        public void AddEmployeeService(int serviceId, int employeeId)
        {
            var employeeService = new EmployeeService 
            { 
                ServiceId = serviceId, 
                EmployeeId = employeeId, 
                DurationInMinutes = GConst.ValidServiceDuration
            };
            context.EmployeeServices.Add(employeeService);
            context.SaveChanges();
        }

        public void AddLocationIndustry(int industryId, int locationId)
        {
            var locationIndustry = new LocationIndustry { IndustryId = industryId, LocationId = locationId };
            context.LocationIndustries.Add(locationIndustry);
            context.SaveChanges();
        }

        public void AddAdministration()
        {
            var admins = new List<Administration>
            {
                 new Administration { Id = 1, Name = "Appointment Duration in Minutes [Default:30]",   Value = "30"},
                 new Administration { Id = 2, Name = "Working Hours Start in 24-Hour Format [Default:8]",   Value = "8"},
                 new Administration { Id = 3, Name = "Working Hours End in 24-Hour Format [Default:18]",   Value = "18"},
            };

            context.Administrations.AddRange(admins);
            context.SaveChanges();
        }
    }
}