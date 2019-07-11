namespace Studio.Application.Industries.Queries.GetIndustryById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetIndustryByIdQueryHandler : IRequestHandler<GetIndustryByIdQuery, IndustryViewModel>
    {
        private readonly IStudioDbContext context;

        public GetIndustryByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<IndustryViewModel> Handle(GetIndustryByIdQuery request, CancellationToken cancellationToken)
        {
            var industry = await context.Industries.FindAsync(request.Id);

            if (industry == null)
            {
                throw new NotFoundException(GConst.Industry, request.Id);
            }

            return IndustryViewModel.Create(industry);
        }
    }
}