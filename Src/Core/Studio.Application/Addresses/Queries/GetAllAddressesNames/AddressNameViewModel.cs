namespace Studio.Application.Addresses.Queries.GetAllAddressesNames
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class AddressNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Address, AddressNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => $"гр. {src.City.Name}, {src.AddressFormat.ToString()}"));
        }
    }
}
