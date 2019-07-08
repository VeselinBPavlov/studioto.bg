namespace Studio.Domain.Entities
{
    using Studio.Domain.Enumerations;
    using Studio.Domain.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Location : IAuditInfo, IDeletableEntity
    {
        public Location()
        {
            this.Employees = new HashSet<Employee>();
            this.LocationIndustries = new HashSet<LocationIndustry>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsOffice { get; set; }

        public Workday StartDay { get; set; }

        public Workday EndDay { get; set; }

        public DateTime StartHour { get; set; }

        public DateTime EndHour { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Employee> Employees { get; private set; }

        public virtual ICollection<LocationIndustry> LocationIndustries { get; private set; }

    }
}
