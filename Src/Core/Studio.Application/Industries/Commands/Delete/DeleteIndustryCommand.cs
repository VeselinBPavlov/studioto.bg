namespace Studio.Application.Industries.Commands.Delete
{
    using MediatR;
    
    public class DeleteIndustryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
