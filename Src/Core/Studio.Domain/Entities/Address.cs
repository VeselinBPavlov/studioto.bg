namespace Studio.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string Apartment { get; set; }

        public string Floor { get; set; }

        public string Number { get; set; }

        public string Street { get; set; }

        public string District { get; set; } 

        public string PostalCode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public Location Location { get; set; }
    }
}
