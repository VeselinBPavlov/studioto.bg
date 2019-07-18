namespace Studio.Application.Cities.Queries.GetEmployeesByLocation
{
    using MediatR;

    public class GetEmployeesByLocationListQuery : IRequest<EmployeesByLocationListViewModel>
    {
        public int LocationId { get; set; }
    }
}