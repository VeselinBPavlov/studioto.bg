namespace Studio.Domain.Entities
{
    using System;
    using Studio.Domain.Interfaces;

    public class Appointment : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime ReservationTime { get; set; }

        public string Comment { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public string UserId { get; set; }

        public virtual StudioUser User { get; set; }
    }
}
