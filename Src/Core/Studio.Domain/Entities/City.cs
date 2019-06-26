﻿namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Studio.Domain.Interfaces;

    public class City : IAuditInfo, IDeletableEntity
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
