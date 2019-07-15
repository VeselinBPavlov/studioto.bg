namespace Studio.Application.Cities.Queries.GetEmployeeServiceById
{
    using MediatR;

    public class GetEmployeeServiceByIdQuery : IRequest<EmployeeServiceViewModel>
    {
        public int EmployeeId { get; set; }

        public int ServiceId { get; set; }

    }
}