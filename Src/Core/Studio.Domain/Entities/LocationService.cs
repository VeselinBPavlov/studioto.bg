namespace Studio.Domain.Entities
{
    public class LocationService
    {
        public bool IsActive { get; set; }

        public decimal Price { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
    }
}
