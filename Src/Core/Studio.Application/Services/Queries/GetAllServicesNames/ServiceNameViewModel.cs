namespace Studio.Application.Services.Queries.GetAllServicesNames
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class ServiceNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Service, ServiceNameViewModel>();
        }
    }
}
