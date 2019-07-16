namespace Studio.Application.Addresses.Queries.GetAllAddresses
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllAddressesListQueryHandler : IRequestHandler<GetAllAddressesListQuery, AddressesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllAddressesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AddressesListViewModel> Handle(GetAllAddressesListQuery request, CancellationToken cancellationToken)
        {
            return new AddressesListViewModel
            {
                Addresses = await this.context.Addresses.Where(a => a.IsDeleted != true).ProjectTo<AddressAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}