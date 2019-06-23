namespace Studio.Application.Industries.Commands.Create
{
    using MediatR;

    public class CreateClientCommand : IRequest
    {
        public string Name { get; set; }
    }
}
