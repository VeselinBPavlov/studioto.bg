namespace Studio.Application.Cities.Queries.GetLocationIndustryById
{
    using MediatR;

    public class GetLocationIndustryByIdQuery : IRequest<LocationIndustryViewModel>
    {
        public int LocationId { get; set; }

        public int IndustryId { get; set; }
    }
}