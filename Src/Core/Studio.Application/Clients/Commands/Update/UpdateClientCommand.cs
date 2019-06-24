namespace Studio.Application.Industries.Commands.Update
{
    using MediatR;

    public class UpdateClientCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
