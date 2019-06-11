namespace Studio.Domain.Entities
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class StudioUser : IdentityUser
    {
        public StudioUser()
        {
            this.Appointments = new HashSet<Appointment>();
        }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        
        public string Phone { get; set; }

        public ICollection<Appointment> Appointments { get; private set; }
    }
}
