namespace Studio.Application.Appointments.Commands.Update
{
    using MediatR;
    using System;

    public class UpdateAppointmentCommand : IRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime ReservetionDate { get; set; }

        public DateTime ReservetionTime { get; set; }

        public string TimeBlockHelper { get; set; }

        public string Comment { get; set; }

        public int ServiceId { get; set; }

        public int EmployeeId { get; set; }
    }
}
