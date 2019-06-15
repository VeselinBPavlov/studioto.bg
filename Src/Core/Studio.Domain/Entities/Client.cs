namespace Studio.Domain.Entities
{
    using Studio.Domain.Enumerations;
    using System.Collections.Generic;
    
    public class Client
    {
        public Client()
        {
            this.Locations = new HashSet<Location>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Industry Industry { get; set; }
        
        public Address Address { get; set; }

        public virtual ICollection<Location> Locations { get; private set; }
    }
}
