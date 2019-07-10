namespace Studio.Application.Clients.Queries.GetClientById
{
    using MediatR;

    public class GetClientByIdQuery : IRequest<ClientViewModel>
    {
        public int Id { get; set; }
    }
}