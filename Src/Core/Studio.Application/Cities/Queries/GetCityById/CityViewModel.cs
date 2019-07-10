namespace Studio.Application.Cities.Queries.GetCityById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public static Expression<Func<City, CityViewModel>> Projection
        {
            get
            {
                return city => new CityViewModel
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryId = city.CountryId,
                    CountryName = city.Country.Name
                };
            }
        }

        public static CityViewModel Create(City city)
        {
            return Projection.Compile().Invoke(city);
        }
    }
}