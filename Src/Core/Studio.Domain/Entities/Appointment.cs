namespace Studio.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string UserId { get; set; }
        public StudioUser User { get; set; }
    }
}
