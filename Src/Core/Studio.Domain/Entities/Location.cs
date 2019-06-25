namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Location
    {
        public Location()
        {
            this.Employees = new HashSet<Employee>();
            this.LocationIndustries = new HashSet<LocationIndustry>();
            this.Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsOffice { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual LocationMapData LocationMapData { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Employee> Employees { get; private set; }

        public virtual ICollection<LocationIndustry> LocationIndustries { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; }
    }
}
