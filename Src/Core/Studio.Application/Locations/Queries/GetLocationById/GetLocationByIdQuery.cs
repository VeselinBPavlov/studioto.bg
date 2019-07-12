namespace Studio.Application.Locations.Queries.GetLocationById
{
    using MediatR;

    public class GetLocationByIdQuery : IRequest<LocationViewModel>
    {
        public int Id { get; set; }
    }
}