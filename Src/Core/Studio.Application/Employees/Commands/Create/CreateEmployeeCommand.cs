namespace Studio.Application.Employees.Commands.Create
{
    using MediatR;

    public class CreateEmployeeCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LocationId { get; set; }
    }
}
