namespace Studio.Application.Tests.Infrastructure
{
    using Studio.Domain.Entities;
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
    }
}
