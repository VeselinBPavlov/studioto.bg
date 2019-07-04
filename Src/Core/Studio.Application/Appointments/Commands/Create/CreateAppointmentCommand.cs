namespace Studio.Application.Appointments.Commands.Create
{
    using MediatR;
    using System;

    public class CreateAppointmentCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime ReservetionTime { get; set; }

        public string Comment { get; set; }

        public int ServiceId { get; set; }

        public int EmployeeId { get; set; }

        public string UserId { get; set; }
    }
}
