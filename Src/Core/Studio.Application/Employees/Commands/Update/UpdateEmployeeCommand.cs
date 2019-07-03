namespace Studio.Application.Employees.Commands.Update
{
    using MediatR;

    public class UpdateEmployeeCommand : IRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LocationId { get; set; }
    }
}
