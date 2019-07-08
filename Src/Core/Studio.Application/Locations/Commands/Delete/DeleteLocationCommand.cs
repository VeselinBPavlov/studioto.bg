namespace Studio.Application.Locations.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteLocationCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
