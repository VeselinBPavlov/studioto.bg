namespace Studio.Domain.Entities
{
    using System;
    using Studio.Domain.Interfaces;

    public class EmployeeService : IAuditInfo, IDeletableEntity
    {
        public decimal Price { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
    }
}
