namespace Studio.Application.Employees.Commands.Delete
{
    using MediatR;

    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
