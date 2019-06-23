namespace Studio.Domain.Entities
{
    public class EmployeeService
    {
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
    }
}
