namespace Studio.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Studio.Domain.Interfaces;

    public class Employee : IAuditInfo, IDeletableEntity
    {
        public Employee()
        {
            this.Appointments = new HashSet<Appointment>();
            this.EmployeeServices = new HashSet<EmployeeService>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<EmployeeService> EmployeeServices { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; }
    }
}
