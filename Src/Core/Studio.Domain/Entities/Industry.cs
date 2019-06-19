namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Industry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
