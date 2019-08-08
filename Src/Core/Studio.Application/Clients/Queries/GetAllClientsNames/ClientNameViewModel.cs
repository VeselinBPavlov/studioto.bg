namespace Studio.Application.Clients.Queries.GetAllClientsNames
{
    using AutoMapper;
    using Interfaces.Mapping;
    using Domain.Entities;

    public class ClientNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Client, ClientNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.CompanyName));
        }
    }
}
