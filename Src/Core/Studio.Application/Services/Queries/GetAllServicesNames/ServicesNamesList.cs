namespace Studio.Application.Services.Queries.GetAllNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Domain.Entities;
    using Interfaces.Mapping;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ServicesNamesListViewModel
    {
        public IList<ServiceNameViewModel> Services { get; set; }        
    }

    public class ServiceNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Service, ServiceNameViewModel>();
        }
    }

    public class GetServicesNamesListQuery : IRequest<ServicesNamesListViewModel>
    {
    }

    public class GetServicesNamesListQueryHandler : IRequestHandler<GetServicesNamesListQuery, ServicesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetServicesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServicesNamesListViewModel> Handle(GetServicesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new ServicesNamesListViewModel
            {
                Services = await this.context.Services.Where(c => c.IsDeleted != true).OrderBy(x => x.Name).ProjectTo<ServiceNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
