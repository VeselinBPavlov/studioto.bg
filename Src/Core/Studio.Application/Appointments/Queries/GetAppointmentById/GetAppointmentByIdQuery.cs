namespace Studio.Application.Appointments.Queries.GetAppointmentById
{
    using MediatR;

    public class GetAppointmentByIdQuery : IRequest<AppointmentViewModel>
    {
        public int Id { get; set; }
    }
}