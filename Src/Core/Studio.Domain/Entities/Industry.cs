using System.Collections.Generic;

namespace Studio.Domain.Entities
{
    public class Industry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
