namespace Studio.Application.Interfaces.Core
{
    using System;

    public interface IAppointmentCommand 
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Phone { get; set; }

        DateTime ReservationDate { get; set; }

        DateTime ReservationTime { get; set; }

        string TimeBlockHelper { get; set; }

        string Comment { get; set; }

        int ServiceId { get; set; }

        int EmployeeId { get; set; }

        string UserId { get; set; }
    }
}
