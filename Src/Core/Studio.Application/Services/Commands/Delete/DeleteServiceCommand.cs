namespace Studio.Application.Services.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteServiceCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
