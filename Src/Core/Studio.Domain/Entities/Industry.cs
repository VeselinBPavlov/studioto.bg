namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Studio.Domain.Interfaces;

    public class Industry : IAuditInfo, IDeletableEntity
    {
        public Industry()
        {
            this.Services = new HashSet<Service>();
            this.LocationIndustries = new HashSet<LocationIndustry>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Possition { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Service> Services { get; private set; }

        public virtual ICollection<LocationIndustry> LocationIndustries { get; private set; }
    }
}
