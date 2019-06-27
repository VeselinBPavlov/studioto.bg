namespace Studio.Application.Countries.Commands.Delete
{
    using MediatR;
    
    public class DeleteCountryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
