namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Studio.Domain.Interfaces;

    public class Country : IAuditInfo, IDeletableEntity
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
