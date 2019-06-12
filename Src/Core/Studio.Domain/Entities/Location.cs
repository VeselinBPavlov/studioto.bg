namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Location
    {
        public Location()
        {
            this.Employees = new HashSet<Employee>();
            this.LocationServices = new HashSet<LocationService>();
            this.Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int LocationMapDataId { get; set; }
        public LocationMapData LocationMapData { get; set; }

        public virtual ICollection<Employee> Employees { get; private set; }

        public virtual ICollection<LocationService> LocationServices { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; }
    }
}
