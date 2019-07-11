namespace Studio.Application.Countries.Queries.GetCountryById
{
    using AutoMapper;
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryViewModel>
    {
        private readonly IStudioDbContext context;

        public GetCountryByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<CountryViewModel> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await context.Countries.FindAsync(request.Id);

            if (country == null)
            {
                throw new NotFoundException(GConst.Country, request.Id);
            }

            return CountryViewModel.Create(country);
        }
    }
}