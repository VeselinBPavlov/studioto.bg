namespace Studio.Domain.Entities
{
    public class LocationService
    {
        public bool IsActive { get; set; }

        public decimal Price { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }
    }
}
