using System;
using System.Linq.Expressions;
using AutoMapper;
using Studio.Application.Interfaces.Mapping;
using Studio.Domain.Entities;

namespace Studio.Application.Countries.Queries.GetAllCountries
{
    public class CountryViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Country, CountryViewModel>();
        }
    }
}