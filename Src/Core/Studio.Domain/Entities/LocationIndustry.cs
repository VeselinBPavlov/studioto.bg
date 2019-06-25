namespace Studio.Domain.Entities
{
    public class LocationIndustry
    {
        public bool IsActive { get; set; }
        

        public string Description { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public int IndustryId { get; set; }

        public virtual Industry Industry { get; set; }
    }
}
