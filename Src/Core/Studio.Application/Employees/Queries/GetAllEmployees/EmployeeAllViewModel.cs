namespace Studio.Application.Employees.Queries.GetAllEmployees
{
    using System.Linq;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class EmployeeAllViewModel : IHaveCustomMapping
    {
         public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public string Possitions { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Employee, EmployeeAllViewModel>()
                .ForMember(x => x.LocationName, y => y.MapFrom(src => src.Location.Name))
                .ForMember(x => x.Possitions, y => y.MapFrom(src => string.Join(", ", src.EmployeeServices.Select(z => z.Service.Industry.Possition))));
        }
    }
}