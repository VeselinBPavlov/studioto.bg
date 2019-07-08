namespace Studio.Application.Addresses.Commands.Delete
{
    using MediatR;
    using Studio.Application.Interfaces.Core;

    public class DeleteAddressCommand : IRequest, IModifiedCommand
    {
        public int Id { get; set; }
    }
}
