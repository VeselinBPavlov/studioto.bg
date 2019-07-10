namespace Studio.Application.Countries.Queries.GetCountryById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class CountryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Expression<Func<Country, CountryViewModel>> Projection
        {
            get
            {
                return Country => new CountryViewModel
                {
                    Id = Country.Id,
                    Name = Country.Name
                };
            }
        }

        public static CountryViewModel Create(Country country)
        {
            return Projection.Compile().Invoke(country);
        }
    }
}