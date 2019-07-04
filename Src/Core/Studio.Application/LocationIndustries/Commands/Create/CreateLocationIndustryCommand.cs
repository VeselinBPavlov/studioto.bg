namespace Studio.Application.LocationIndustries.Commands.Create
{
    using MediatR;

    public class CreateLocationIndustryCommand : IRequest
    {
        public int LocationId { get; set; }

        public int IndustryId { get; set; }

        public string Description { get; set; }
    }
}
