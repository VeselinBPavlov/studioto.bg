namespace Studio.Application.Clients.Queries.GetAllClientsNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetClientsNamesListQueryHandler : IRequestHandler<GetClientsNamesListQuery, ClientsNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetClientsNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ClientsNamesListViewModel> Handle(GetClientsNamesListQuery request, CancellationToken cancellationToken)
        {
            return new ClientsNamesListViewModel
            {
                Clients = await this.context.Clients.Where(c => c.IsDeleted != true).OrderBy(x => x.CompanyName).ProjectTo<ClientNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
