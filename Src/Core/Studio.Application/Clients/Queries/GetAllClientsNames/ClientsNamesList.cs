namespace Studio.Application.Clients.Queries.GetAllNames
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ClientsNamesListViewModel
    {
        public IList<ClientNameViewModel> Clients { get; set; }        
    }

    public class ClientNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Client, ClientNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.CompanyName));
        }
    }

    public class GetClientsNamesListQuery : IRequest<ClientsNamesListViewModel>
    {
    }

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
                Clients = await this.context.Clients.ProjectTo<ClientNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
