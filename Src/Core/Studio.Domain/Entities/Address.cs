namespace Studio.Domain.Entities
{
    using Studio.Domain.ValueObjects;

    public class Address
    {
        public int Id { get; set; }

        public AddressFormat AddressFormated { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public Location Location { get; set; }
    }
}
