namespace Studio.Application.Locations.Queries.GetFilteredLocations
{
    using MediatR;

    public class GetFilteredLocationsListQuery : IRequest<LocationsFilteredListViewModel>
    {
        public int? CityId { get; set; }

        public string StudioName { get; set; }

        public string ServiceName { get; set; }

        public bool? IsHomePage { get; set; }
    }
}