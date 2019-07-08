namespace Studio.Application.Appointments.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteAppointmentCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
