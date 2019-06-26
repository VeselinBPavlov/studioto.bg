namespace Studio.Domain.Entities
{
    using System;
    using Studio.Domain.Interfaces;

    public class ContactForm : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Topic { get; set; }     

        public string Message { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}