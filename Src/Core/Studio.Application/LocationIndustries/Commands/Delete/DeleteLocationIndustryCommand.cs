namespace Studio.Application.LocationIndustries.Commands.Delete
{
    using MediatR;

    public class DeleteLocationIndustryCommand : IRequest
    {
        public int LocationId { get; set; }

        public int IndustryId { get; set; }
    }
}
