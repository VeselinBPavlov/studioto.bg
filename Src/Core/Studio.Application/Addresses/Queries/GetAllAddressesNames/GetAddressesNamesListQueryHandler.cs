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

    public class GetAddressesNamesListQueryHandler : IRequestHandler<GetAddressesNamesListQuery, AddressesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAddressesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AddressesNamesListViewModel> Handle(GetAddressesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new AddressesNamesListViewModel
            {
                Addresses = await this.context.Addresses.Where(a => (a.Location == null || a.Location.IsDeleted == true) && a.IsDeleted != true).OrderBy(x => x.City.Name).ProjectTo<AddressNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
