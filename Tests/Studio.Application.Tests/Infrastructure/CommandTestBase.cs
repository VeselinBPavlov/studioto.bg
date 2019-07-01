namespace Studio.Application.Tests.Infrastructure
{
    using System;
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
            City city;
            if (countryId == null)
            {
                city = new City { Name = GConst.ValidName };
            }
            else
            {
                city = new City { Name = GConst.ValidName, CountryId = countryId.Value };
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

            Address address;
            if (cityId == null)
            {
                address = new Address { AddressFormat = AddressFormat.For(inputAddressData) };
            }
            else
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
            Location location;
            if (clientId != null)
            {
                location = new Location { Name = GConst.ValidName, ClientId = clientId.Value };
            }
            else if (addressId != null)
            {
                location = new Location { Name = GConst.ValidName, AddressId = addressId.Value };
            }
            else
            {
                location = new Location { Name = GConst.ValidName };
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
            Service service;
            if (industryId != null)
            {
                service = new Service { Name = GConst.ValidName, IndustryId = industryId.Value };
            }
            else
            {
                service = new Service { Name = GConst.ValidName };
            }

            context.Services.Add(service);
            context.SaveChangesAsync();

            var serviceId = context.Services.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            return serviceId;
        }

        public int GetEmployeeId()
        {
            var employee = new Employee { FirstName = GConst.ValidName };
            

            context.Employees.Add(employee);
            context.SaveChangesAsync();

            var employeeId = context.Employees.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            return employeeId;
        }

        public void AddLocationIndustry(int industryId, int locationId)
        {
            var locationIndustry = new LocationIndustry { IndustryId = industryId, LocationId = locationId };
            context.LocationIndustries.Add(locationIndustry);
            context.SaveChanges();
        }

        public void AddAppointment(int serviceId)
        {
            var appointment = new Appointment { ServiceId = serviceId };
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }

        public void AddEmployeeService(int serviceId, int employeeId)
        {
            var employeeService = new EmployeeService { ServiceId = serviceId, EmployeeId = employeeId};
            context.EmployeeServices.Add(employeeService);
            context.SaveChanges();
        }
    }
}