namespace Studio.Application.EmployeeServices.Queries.GetServicesByEmployeeId
{
    using MediatR;

    public class GetServicesByEmployeeIdListQuery : IRequest<ServicesByEmployeeIdListViewModel>
    {
        public int EmployeeId { get; set; }
    }
}