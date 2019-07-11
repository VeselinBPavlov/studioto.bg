namespace Studio.Application.Tests.Infrastructure
{
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Domain.Enumerations;
    using Studio.Domain.ValueObjects;
    using Studio.Persistence.Context;
    using System;
    using System.Collections.Generic;

    public static class QueryArrangeHelper
    {
        public static void AddCountries(StudioDbContext context)
        {
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "Bulgaria" },
                new Country { Id = 2, Name = "France" },
                new Country { Id = 3, Name = "England" },
            };
            context.Countries.AddRange(countries);
            context.SaveChanges();
        }

        public static void AddAppointmentes(StudioDbContext context)
        {
            var userId = CommandArrangeHelper.GetUserId(context);
            var employeeId = CommandArrangeHelper.GetEmployeeId(context, null);
            var serviceId = CommandArrangeHelper.GetServiceId(context, null);

            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, FirstName = "Misho", LastName = "Mishov", Email = "abv@abv.bg", Phone = "0883654321", Comment = "Be Happy!", ReservationDate = new DateTime(2019, 09, 15), TimeBlockHelper = "9", ReservationTime = new DateTime(2019, 09, 15, 9, 0, 0), EmployeeId = employeeId, ServiceId = serviceId, UserId = userId  },
                new Appointment { Id = 2, FirstName = "Pesho", LastName = "Peshov", Email = "abv@abv.bg", Phone = "0883654321", Comment = "Be Happy!", ReservationDate = new DateTime(2019, 09, 15), TimeBlockHelper = "9", ReservationTime = new DateTime(2019, 09, 15, 9, 0, 0), EmployeeId = employeeId, ServiceId = serviceId, UserId = userId  },
                new Appointment { Id = 3, FirstName = "Gosho", LastName = "Goshov", Email = "abv@abv.bg", Phone = "0883654321", Comment = "Be Happy!", ReservationDate = new DateTime(2019, 09, 15), TimeBlockHelper = "9", ReservationTime = new DateTime(2019, 09, 15, 9, 0, 0), EmployeeId = employeeId, ServiceId = serviceId, UserId = userId  }
            };

            context.Appointments.AddRange(appointments);
            context.SaveChanges();
        }

        public static void AddAddresses(StudioDbContext context)
        {
            InputAddressData addressData = new InputAddressData
            {
                Street = "Vasil Levski",
                Number = "10",
                Building = "3G",
                Entrance = "B"
            };

            var cityId = CommandArrangeHelper.GetCityId(context, null);

            var addresses = new List<Address>
            {
                new Address { Id = 1, AddressFormat = (AddressFormat)addressData, Longitude = 40.35M, Latitude = 56.23M, CityId = cityId },
                new Address { Id = 2, AddressFormat = (AddressFormat)addressData, Longitude = 40.35M, Latitude = 56.23M, CityId = cityId },
                new Address { Id = 3, AddressFormat = (AddressFormat)addressData,  Longitude = 40.35M, Latitude = 56.23M, CityId = cityId }
            };
            context.Addresses.AddRange(addresses);
            context.SaveChanges();
        }

        public static void AddClients(StudioDbContext context)
        {
            var clients = new List<Client>
            {
                new Client { Id = 1, CompanyName = "Saloon 5", VatNumber = "124556632", Manager = (Manager)"Ivan Petrov", Phone = "+359888444666" },
                new Client { Id = 2, CompanyName = "Beauty", VatNumber = "245653958", Manager = (Manager)"Petar Ivanov", Phone = "+359889659656"},
                new Client { Id = 3, CompanyName = "Gilly", VatNumber = "BG145856354", Manager = (Manager)"Gosho Petrov", Phone = "+359888656454" },
            };
            context.Clients.AddRange(clients);
            context.SaveChanges();
        }

        public static void AddCities(StudioDbContext context)
        {
            var countryId = CommandArrangeHelper.GetCountryId(context);

            var cities = new List<City>
            {
                new City { Id = 1, Name = "Sofia", CountryId = countryId },
                new City { Id = 2, Name = "Varna", CountryId = countryId},
                new City { Id = 3, Name = "Burgas", CountryId = countryId },
            };
            context.Cities.AddRange(cities);
            context.SaveChanges();
        }

        public static void AddLocations(StudioDbContext context)
        {
            var cityId = CommandArrangeHelper.GetCityId(context, null);

            var addressIdFirst = CommandArrangeHelper.GetAddressId(context, cityId);

            var locations = new List<Location>
            {
                new Location { Id = 1, Name = GConst.ValidName, StartDay = Workday.Понеделник, EndDay = Workday.Петък, StartHour = "9", EndHour = "18", Phone = "0888777666", AddressId = addressIdFirst }
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();
        }

        public static void AddEmployees(StudioDbContext context)
        {
            var industryId = CommandArrangeHelper.GetIndustryId(context);
            var serviceId = CommandArrangeHelper.GetServiceId(context, industryId);
            var addressId = CommandArrangeHelper.GetAddressId(context, null);
            var locationId = CommandArrangeHelper.GetLocationId(context, null, addressId);

            var employee = new Employee { Id = 1, FirstName = GConst.ValidName, LastName = GConst.ValidName, LocationId = locationId };
            context.Employees.Add(employee);
            var employeeService = new EmployeeService { ServiceId = serviceId, EmployeeId = employee.Id };
            context.EmployeeServices.Add(employeeService);
            context.SaveChanges();
        }
    }
}
