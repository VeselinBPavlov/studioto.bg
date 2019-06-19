namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class City
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
