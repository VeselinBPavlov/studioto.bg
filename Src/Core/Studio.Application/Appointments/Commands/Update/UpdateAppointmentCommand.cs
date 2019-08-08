namespace Studio.Application.Appointments.Commands.Update
{
    using System;
    using Interfaces.Core;
    using MediatR;

    public class UpdateAppointmentCommand : IRequest, IAppointmentCommand, IModifiedCommand
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime? ReservationTime { get; set; }

        public string TimeBlockHelper { get; set; }

        public string Comment { get; set; }

        public int ServiceId { get; set; }

        public int EmployeeId { get; set; }

        public string UserId { get; set; }
    }
}
