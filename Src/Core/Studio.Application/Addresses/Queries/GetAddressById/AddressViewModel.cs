namespace Studio.Application.Addresses.Queries.GetAddressById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class AddressViewModel
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string District { get; set; }

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
                    Street = address.AddressFormat.Street,
                    Number = address.AddressFormat.Number,
                    Building = address.AddressFormat.Building,
                    Entrance = address.AddressFormat.Entrance,
                    Floor = address.AddressFormat.Floor,
                    Apartment = address.AddressFormat.Apartment,
                    District = address.AddressFormat.District,
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