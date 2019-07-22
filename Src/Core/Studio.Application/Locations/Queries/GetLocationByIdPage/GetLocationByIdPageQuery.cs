namespace Studio.Application.Locations.Queries.GetLocationByIdPage
{
    using MediatR;

    public class GetLocationByIdPageQuery : IRequest<LocationPageViewModel>
    {
        public int Id { get; set; }
    }
}