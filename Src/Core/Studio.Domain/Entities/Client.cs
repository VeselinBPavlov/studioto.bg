﻿namespace Studio.Domain.Entities
{
    using Studio.Domain.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Client : IAuditInfo, IDeletableEntity
    {
        public Client()
        {
            this.Locations = new HashSet<Location>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Location> Locations { get; private set; }
    }
}
