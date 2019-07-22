namespace Studio.Application.Locations.Queries.GetLocationByIdPage
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

    public class GetLocationByIdPageQueryHandler : IRequestHandler<GetLocationByIdPageQuery, LocationPageViewModel>
    {
        private readonly IStudioDbContext context;

        public GetLocationByIdPageQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<LocationPageViewModel> Handle(GetLocationByIdPageQuery request, CancellationToken cancellationToken)
        {
            var location = await this.context.Locations
                .Include(l => l.Address)
                    .ThenInclude(a => a.City)
                .Include(l => l.Employees)
                    .ThenInclude(e => e.EmployeeServices)
                        .ThenInclude(es => es.Service)
                            .ThenInclude(s => s.Industry)
                .Include(l => l.LocationIndustries)
                    .ThenInclude(li => li.Industry)
                .SingleOrDefaultAsync(c => c.Id == request.Id);

            if (location == null)
            {
                throw new NotFoundException(GConst.Location, request.Id);
            }

            return LocationPageViewModel.Create(location);
        }
    }
}