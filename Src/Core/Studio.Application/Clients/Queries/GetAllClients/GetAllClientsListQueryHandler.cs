namespace Studio.Application.Clients.Queries.GetAllClients
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllClientsListQueryHandler : IRequestHandler<GetAllClientsListQuery, ClientsListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllClientsListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ClientsListViewModel> Handle(GetAllClientsListQuery request, CancellationToken cancellationToken)
        {
            return new ClientsListViewModel
            {
                Clients = await this.context.Clients.ProjectTo<ClientAllViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}