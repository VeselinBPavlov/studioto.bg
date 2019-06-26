namespace Studio.Domain.Entities
{
    using System;
    using Studio.Domain.Interfaces;
    using Studio.Domain.ValueObjects;

    public class Address : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public virtual AddressFormat AddressFormat { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

         public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual Location Location { get; set; }
    }
}
