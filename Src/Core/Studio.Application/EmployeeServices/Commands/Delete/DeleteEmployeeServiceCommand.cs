namespace Studio.Application.EmployeeServices.Commands.Delete
{
    using MediatR;

    public class DeleteEmployeeServiceCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int ServiceId { get; set; }
    }
}
