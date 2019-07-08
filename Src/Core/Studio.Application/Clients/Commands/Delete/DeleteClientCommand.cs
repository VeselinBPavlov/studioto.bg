namespace Studio.Application.Clients.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteClientCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
