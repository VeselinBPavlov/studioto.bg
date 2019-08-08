namespace Studio.Application.Addresses.Queries.GetAllAddressesNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAddressesNamesForEditListQueryHandler : IRequestHandler<GetAddressesNamesForEditListQuery, AddressesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAddressesNamesForEditListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AddressesNamesListViewModel> Handle(GetAddressesNamesForEditListQuery request, CancellationToken cancellationToken)
        {
            return new AddressesNamesListViewModel
            {
                Addresses = await this.context.Addresses.Where(a => (a.Location == null || a.Location.IsDeleted == true || a.Location.Id == request.LocationId) && a.IsDeleted != true).OrderBy(x => x.City.Name).ProjectTo<AddressNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
