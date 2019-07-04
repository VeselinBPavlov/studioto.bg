namespace Studio.Application.LocationIndustries.Commands.Update
{
    using MediatR;

    public class UpdateLocationIndustryCommand : IRequest
    {
        public int LocationId { get; set; }

        public int IndustryId { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }
    }
}
