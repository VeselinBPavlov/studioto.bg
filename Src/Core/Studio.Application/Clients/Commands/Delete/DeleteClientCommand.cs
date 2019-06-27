namespace Studio.Application.Clients.Commands.Delete
{
    using MediatR;

    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
    }
}
