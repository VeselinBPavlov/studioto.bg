namespace Studio.Application.Employees.Queries.GetAllEmployeeNames
{
    using AutoMapper;
    using Interfaces.Mapping;
    using Domain.Entities;

    public class EmployeeNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Employee, EmployeeNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
