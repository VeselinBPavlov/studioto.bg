namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Studio.Domain.Interfaces;

    public class Service : IAuditInfo, IDeletableEntity
    {
        public Service()
        {
            this.LocationServices = new HashSet<EmployeeService>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int IndustryId { get; set; }

        public virtual Industry Industry { get; set; }

        public virtual ICollection<EmployeeService> LocationServices { get; private set; }
    }
}
