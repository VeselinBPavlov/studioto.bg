namespace Studio.Application.EmployeeServices.Queries.GetAllEmployeeServices
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class EmployeeServiceAllViewModel : IHaveCustomMapping
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string Price { get; set; }

        public string DurationInMinutes { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EmployeeService, EmployeeServiceAllViewModel>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
        }
    }
}