namespace Studio.Application.Cities.Queries.GetEmployeeById
{
    using MediatR;

    public class GetEmployeeByIdQuery: IRequest<EmployeeProfileViewModel>
    {
        public int Id { get; set; }
    }
}