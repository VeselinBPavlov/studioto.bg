namespace Studio.Application.Addresses.Queries.GetAllAddresses
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class AddressAllViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Address, AddressAllViewModel>()
                .ForMember(x => x.Address, y => y.MapFrom(src => src.AddressFormat.ToString()))
                .ForMember(x => x.CityName, y => y.MapFrom(src => src.City.Name));
        }
    }
}