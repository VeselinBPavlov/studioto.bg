namespace Studio.Application.EmployeeServices.Queries.GetServicesByEmployeeId
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class ServiceByEmployeeIdViewModel : IHaveCustomMapping
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EmployeeService, ServiceByEmployeeIdViewModel>()
                .ForMember(x => x.ServiceName, y => y.MapFrom(src => src.Service.Name + " - " + src.Price + " лв."));
        }
    }
}