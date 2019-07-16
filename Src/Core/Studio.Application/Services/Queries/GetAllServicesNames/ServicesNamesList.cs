namespace Studio.Application.Services.Queries.GetAllNames
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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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
                Services = await this.context.Services.Where(c => c.IsDeleted != true).ProjectTo<ServiceNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
