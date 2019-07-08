namespace Studio.Application.Employees.Commands.Update
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class UpdateEmployeeCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LocationId { get; set; }
    }
}
