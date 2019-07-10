namespace Studio.Application.Cities.Queries.GetCityById
{
    using MediatR;

    public class GetCityByIdQuery : IRequest<CityViewModel>
    {
        public int Id { get; set; }
    }
}