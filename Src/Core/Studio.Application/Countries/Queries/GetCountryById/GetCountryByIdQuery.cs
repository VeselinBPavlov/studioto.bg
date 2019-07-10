namespace Studio.Application.Countries.Queries.GetCountryById
{
    using MediatR;

    public class GetCountryByIdQuery : IRequest<CountryViewModel>
    {
        public int Id { get; set; }
    }
}