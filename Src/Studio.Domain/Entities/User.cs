namespace Studio.Domain.Entities
{
    using System.Collections.Generic;

    using Enums;

    public class User
    {
        public User()
        {
            this.Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Appointment> Appointments { get; private set; }
    }
}
