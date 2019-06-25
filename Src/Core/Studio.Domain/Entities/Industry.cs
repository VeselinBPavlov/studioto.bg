namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Industry
    {
        public Industry()
        {
            this.Services = new HashSet<Service>();
            this.LocationIndustries = new HashSet<LocationIndustry>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; private set; }

        public virtual ICollection<LocationIndustry> LocationIndustries { get; private set; }
    }
}
