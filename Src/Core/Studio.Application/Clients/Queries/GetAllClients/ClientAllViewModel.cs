namespace Studio.Application.Clients.Queries.GetAllClients
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class ClientAllViewModel : IHaveCustomMapping
    {
         public int Id { get; set; }

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public string ManagerNames { get; set; }

        public string Phone { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Client, ClientAllViewModel>()
                .ForMember(x => x.ManagerNames, y => y.MapFrom(src => src.Manager.ToString()));
        }
    }
}