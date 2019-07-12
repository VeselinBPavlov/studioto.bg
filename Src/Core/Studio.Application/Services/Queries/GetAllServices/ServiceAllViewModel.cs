namespace Studio.Application.Services.Queries.GetAllServices
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class ServiceAllViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IndustryId { get; set; }

        public string IndustryName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Service, ServiceAllViewModel>()
                .ForMember(x => x.IndustryName, y => y.MapFrom(src => src.Industry.Name));
        }
    }
}