namespace Studio.Application.Addresses.Queries.GetAddressById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class AddressViewModel
    {
        public int Id { get; set; }

        public string AddressFormat { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public bool HasLocation { get; set; }

        public static Expression<Func<Address, AddressViewModel>> Projection
        {
            get
            {
                return address => new AddressViewModel
                {
                    Id = address.Id,
                    AddressFormat = address.AddressFormat.ToString(),
                    Longitude = address.Longitude,
                    Latitude = address.Latitude,
                    CityId = address.CityId,
                    CityName = address.City.Name                   
                };
            }
        }

        public static AddressViewModel Create(Address address)
        {
            return Projection.Compile().Invoke(address);
        }
    }
}