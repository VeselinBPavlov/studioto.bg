﻿namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class StudioUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public StudioUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Appointments = new HashSet<Appointment>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsTemporary { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Appointment> Appointments { get; private set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; private set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; private set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; private set; }
    }
}
