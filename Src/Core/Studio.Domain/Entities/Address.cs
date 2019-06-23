namespace Studio.Domain.Entities
{
    using Studio.Domain.ValueObjects;

    public class Address
    {
        public int Id { get; set; }

        public virtual AddressFormat AddressFormated { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual Location Location { get; set; }
    }
}
