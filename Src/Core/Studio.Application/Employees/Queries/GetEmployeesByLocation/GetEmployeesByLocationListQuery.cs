namespace Studio.Application.Cities.Queries.GetEmployeesByLocation
{
    using MediatR;

    public class GetEmployeesByLocationListQuery: IRequest<EmployeesListViewModel>
    {
        public int LocationId { get; set; }
    }
}