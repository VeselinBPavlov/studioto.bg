namespace Studio.Application.Addresses.Commands.Delete
{
    using MediatR;

    public class DeleteAddressCommand : IRequest
    {
        public int Id { get; set; }
    }
}
