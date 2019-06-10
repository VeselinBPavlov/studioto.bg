namespace Studio.Domain.Entities
{
    public class LocationService
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public decimal Price { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
