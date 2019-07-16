namespace Studio.Application.Countries.Queries.GetAllCountries
{
    using MediatR;

    public class GetAllCountriesListQuery : IRequest<CountriesListViewModel>
    {
    }
}