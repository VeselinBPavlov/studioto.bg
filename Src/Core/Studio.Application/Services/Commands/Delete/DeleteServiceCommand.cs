namespace Studio.Application.Services.Commands.Delete
{
    using MediatR;

    public class DeleteServiceCommand : IRequest
    {
        public int Id { get; set; }
    }
}
