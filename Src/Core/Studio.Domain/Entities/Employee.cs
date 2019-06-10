namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    public class Employee
    {
        public Employee()
        {
            this.Appointments = new HashSet<Appointment>();
            this.EmployeeServices = new HashSet<EmployeeService>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<EmployeeService> EmployeeServices { get; private set; }

        public ICollection<Appointment> Appointments { get; private set; }
    }
}
