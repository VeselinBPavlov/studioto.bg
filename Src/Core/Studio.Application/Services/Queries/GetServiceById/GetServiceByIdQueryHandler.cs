namespace Studio.Application.Services.Queries.GetServiceById
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

    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceViewModel>
    {
        private readonly IStudioDbContext context;

        public GetServiceByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<ServiceViewModel> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await this.context.Services.Include(s => s.Industry).SingleOrDefaultAsync(s => s.Id == request.Id);

            if (service == null)
            {
                throw new NotFoundException(GConst.Service, request.Id);
            }

            return ServiceViewModel.Create(service);
        }
    }
}