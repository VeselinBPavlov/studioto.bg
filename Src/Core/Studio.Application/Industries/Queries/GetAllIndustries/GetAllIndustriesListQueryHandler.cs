namespace Studio.Application.Industries.Queries.GetAllIndustries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllIndustriesListQueryHandler : IRequestHandler<GetAllIndustriesListQuery, IndustriesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllIndustriesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IndustriesListViewModel> Handle(GetAllIndustriesListQuery request, CancellationToken cancellationToken)
        {
            return new IndustriesListViewModel
            {
                Industries = await this.context.Industries.Where(i => i.IsDeleted != true).ProjectTo<IndustryAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}