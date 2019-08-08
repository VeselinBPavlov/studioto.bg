namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using Interfaces.Core;
    using MediatR;

    public class CreateAppointmentCommand : IRequest, IAppointmentCommand
    {
        public DateTime ReservationDate { get; set; }

        public DateTime? ReservationTime { get; set; }

        public string TimeBlockHelper { get; set; }

        public string Comment { get; set; }

        public int ServiceId { get; set; }

        public int EmployeeId { get; set; }

        public string UserId { get; set; }
    }
}
