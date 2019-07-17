namespace Studio.Application.Interfaces.Core
{
    using System;

    public interface IAppointmentCommand 
    {
        DateTime ReservationDate { get; set; }

        DateTime? ReservationTime { get; set; }

        string TimeBlockHelper { get; set; }

        string Comment { get; set; }

        int ServiceId { get; set; }

        int EmployeeId { get; set; }

        string UserId { get; set; }
    }
}
