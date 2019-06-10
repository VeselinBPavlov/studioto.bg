namespace Studio.Domain.Entities
{
    using System.Collections.Generic;
    
    public class Client
    {
        public Client()
        {
            this.Locations = new HashSet<Location>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Location> Locations { get; private set; }
    }
}
