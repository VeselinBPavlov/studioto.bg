namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using ValueObjects;

    public class Client : IAuditInfo, IDeletableEntity
    {
        public Client()
        {
            this.Locations = new HashSet<Location>();
        }

        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public Manager Manager { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Location> Locations { get; private set; }
    }
}
