namespace Studio.Application.Employees.Queries.GetEmployeeById
{
    using MediatR;

    public class GetEmployeeByIdQuery : IRequest<EmployeeViewModel>
    {
        public int Id { get; set; }
    }
}