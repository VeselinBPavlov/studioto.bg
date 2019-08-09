namespace Studio.Domain.Entities
{
    using System;
    using Studio.Domain.Interfaces;

    public class EmployeeService : IAuditInfo
    {
        public decimal Price { get; set; }

        public string DurationInMinutes { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
    }
}
