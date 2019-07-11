namespace Studio.Application.Cities.Queries.GetCityById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityViewModel>
    {
        private readonly IStudioDbContext context;

        public GetCityByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<CityViewModel> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await context.Cities.Include(c => c.Country).SingleOrDefaultAsync(c => c.Id == request.Id);

            if (city == null)
            {
                throw new NotFoundException(GConst.City, request.Id);
            }

            return CityViewModel.Create(city);
        }
    }
}