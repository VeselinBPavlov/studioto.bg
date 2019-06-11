﻿namespace Studio.Domain.Entities
{
    using System;
    using Microsoft.AspNetCore.Identity;

    using Interfaces;

    public class StudioRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public StudioRole()
            : this(null)
        {
        }

        public StudioRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
