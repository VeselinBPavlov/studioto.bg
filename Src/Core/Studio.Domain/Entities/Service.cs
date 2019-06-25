namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Service
    {
        public Service()
        {
            this.LocationServices = new HashSet<EmployeeService>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int IndustryId { get; set; }

        public virtual Industry Industry { get; set; }

        public virtual ICollection<EmployeeService> LocationServices { get; private set; }
    }
}
