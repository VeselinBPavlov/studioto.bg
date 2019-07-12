namespace Studio.Application.Employees.Queries.GetAllEmployees
{
    using MediatR;

    public class GetAllEmployeesListQuery: IRequest<EmployeesListViewModel>
    {
    }
}