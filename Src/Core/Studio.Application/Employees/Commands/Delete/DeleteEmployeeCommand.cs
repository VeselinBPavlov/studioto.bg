namespace Studio.Application.Employees.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteEmployeeCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
