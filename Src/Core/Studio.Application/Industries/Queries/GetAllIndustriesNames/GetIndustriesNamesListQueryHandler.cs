namespace Studio.Application.Industries.Queries.GetAllIndustriesNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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
