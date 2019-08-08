namespace Studio.Application.Countries.Queries.GetCountryById
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryViewModel>
    {
        private readonly IStudioDbContext context;

        public GetCountryByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<CountryViewModel> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await this.context.Countries.FindAsync(request.Id);

            if (country == null)
            {
                throw new NotFoundException(GConst.Country, request.Id);
            }

            return CountryViewModel.Create(country);
        }
    }
}