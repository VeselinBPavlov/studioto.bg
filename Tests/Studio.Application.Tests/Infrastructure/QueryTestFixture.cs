namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    public class QueryTestFixture : IDisposable
    {
        protected StudioDbContext context;
        protected IMapper mapper;
        
        public QueryTestFixture()
        {
            context = StudioDBContextFactory.Create();
            mapper = AutoMapperFactory.Create();
        }
        public void Dispose()
        {
            StudioDBContextFactory.Destroy(this.context);
        }

        public void AddCountries() 
        {
            var countries = new List<Country> 
            { 
                new Country { Name = "Bulgaria" },
                new Country { Name = "France" },
                new Country { Name = "England" },
            };            
            this.context.Countries.AddRange(countries);
            this.context.SaveChanges();
        }

        public void AddCities() 
        {
            var country = new Country { Name = "Bulgaria" };
            context.Countries.Add(country);
            context.SaveChanges();

            var cities = new List<City> 
            { 
                new City { Name = "Sofia", CountryId = country.Id },
                new City { Name = "Varna", CountryId = country.Id},
                new City { Name = "Burgas", CountryId = country.Id },
            };            
            this.context.Cities.AddRange(cities);
            this.context.SaveChanges();
        }


    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}