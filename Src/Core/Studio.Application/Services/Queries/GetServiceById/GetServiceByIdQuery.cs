namespace Studio.Application.Services.Queries.GetServiceById
{
    using MediatR;

    public class GetServiceByIdQuery : IRequest<ServiceViewModel>
    {
        public int Id { get; set; }
    }
}