namespace Studio.Application.Industries.Commands.Delete
{
    using MediatR;
    
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
    }
}
