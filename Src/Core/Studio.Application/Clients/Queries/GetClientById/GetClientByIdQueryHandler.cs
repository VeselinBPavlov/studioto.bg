namespace Studio.Application.Clients.Queries.GetClientById
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientViewModel>
    {
        private readonly IStudioDbContext context;

        public GetClientByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<ClientViewModel> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await context.Clients.Include(c => c.Manager).SingleOrDefaultAsync(c => c.Id == request.Id);

            if (client == null)
            {
                throw new NotFoundException(GConst.Client, request.Id);
            }

            return ClientViewModel.Create(client);
        }
    }
}