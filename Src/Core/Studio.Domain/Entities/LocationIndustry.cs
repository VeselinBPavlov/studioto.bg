namespace Studio.Domain.Entities
{
    using System;
    using Studio.Domain.Interfaces;

    public class LocationIndustry : IAuditInfo
    {
        public bool IsActive { get; set; }        

        public string Description { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public int IndustryId { get; set; }

        public virtual Industry Industry { get; set; }
    }
}
