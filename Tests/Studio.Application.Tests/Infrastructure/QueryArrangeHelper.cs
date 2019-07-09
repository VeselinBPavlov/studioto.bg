namespace Studio.Application.Tests.Infrastructure
{
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Domain.Enumerations;
    using Studio.Persistence.Context;
    using System.Collections.Generic;

    public static class QueryArrangeHelper
    {
        public static void AddCountries(StudioDbContext context)
        {
            var countries = new List<Country>
            {
                new Country { Name = "Bulgaria" },
                new Country { Name = "France" },
                new Country { Name = "England" },
            };
            context.Countries.AddRange(countries);
            context.SaveChanges();
        }

        public static void AddCities(StudioDbContext context)
        {
            var countryId = CommandArrangeHelper.GetCountryId(context);

            var cities = new List<City>
            {
                new City { Name = "Sofia", CountryId = countryId },
                new City { Name = "Varna", CountryId = countryId},
                new City { Name = "Burgas", CountryId = countryId },
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
    }
}
