namespace Studio.Application.Appointments.Queries.GetAvailableAppointments
{
    using System;
    using MediatR;
    using Studio.Application.Appointments.Commands.Create;

    public class GetAvailableAppointmentsQuery : IRequest<AvailableAppointmentsViewModel>
    {
        public CreateAppointmentCommand Command { get; set; }
    }
}