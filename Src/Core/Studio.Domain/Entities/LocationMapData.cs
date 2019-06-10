namespace Studio.Domain.Entities
{
    public class LocationMapData
    {
        public int Id { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
