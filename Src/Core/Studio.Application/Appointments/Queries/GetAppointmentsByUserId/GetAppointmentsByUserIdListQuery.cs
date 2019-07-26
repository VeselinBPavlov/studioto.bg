namespace Studio.Application.Appointments.Queries.GetAppointmentsByUserId
{
    using MediatR;

    public class GetAppointmentsByUserIdListQuery : IRequest<AppointmentsProfileListViewModel>
    {
        public string UserId { get; set; }
    }
}