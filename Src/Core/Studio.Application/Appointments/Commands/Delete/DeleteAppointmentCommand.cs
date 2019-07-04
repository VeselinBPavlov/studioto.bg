namespace Studio.Application.Appointments.Commands.Delete
{
    using MediatR;

    public class DeleteAppointmentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
