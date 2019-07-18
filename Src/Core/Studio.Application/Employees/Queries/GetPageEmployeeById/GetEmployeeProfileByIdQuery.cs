namespace Studio.Application.Employees.Queries.GetPageEmployeeById
{
    using MediatR;
    using Studio.Application.Employees.Queries.GetPageEmployeeById;

    public class GetEmployeeProfileByIdQuery : IRequest<EmployeeProfileViewModel>
    {
        public int Id { get; set; }
    }
}