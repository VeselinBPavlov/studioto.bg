namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Service
    {
        public Service()
        {
            this.LocationServices = new HashSet<LocationService>();
        }

        public int Id { get; set; }

        public int Name { get; set; }

        public IEnumerable<LocationService> LocationServices { get; private set; }
    }
}
