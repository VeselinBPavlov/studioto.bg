namespace Studio.Application.Industries.Queries.GetAllNames
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

    public class IndustriesNamesListViewModel
    {
        public IList<IndustryNameViewModel> Industries { get; set; }        
    }

    public class IndustryNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Industry, IndustryNameViewModel>();
        }
    }

    public class GetIndustriesNamesListQuery : IRequest<IndustriesNamesListViewModel>
    {
    }

    public class GetIndustriesNamesListQueryHandler : IRequestHandler<GetIndustriesNamesListQuery, IndustriesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetIndustriesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IndustriesNamesListViewModel> Handle(GetIndustriesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new IndustriesNamesListViewModel
            {
                Industries = await this.context.Industries.Where(c => c.IsDeleted != true).OrderBy(x => x.Name).ProjectTo<IndustryNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
