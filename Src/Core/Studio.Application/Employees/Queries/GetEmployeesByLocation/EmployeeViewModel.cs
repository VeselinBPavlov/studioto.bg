namespace Studio.Application.Cities.Queries.GetEmployeesByLocation
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class EmployeeViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Possitions { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Employee, EmployeeViewModel>()
                .ForMember(x => x.Possitions, y => y.MapFrom(src => string.Join(", ", src.EmployeeServices.Select(z => z.Service.Industry.Possition))));
        }
    }
}