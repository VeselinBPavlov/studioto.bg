namespace Studio.Application.Clients.Queries.GetClientById
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientViewModel>
    {
        private readonly IStudioDbContext context;

        public GetClientByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<ClientViewModel> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients.Include(c => c.Manager).SingleOrDefaultAsync(c => c.Id == request.Id);

            if (client == null)
            {
                throw new NotFoundException(GConst.Client, request.Id);
            }

            return ClientViewModel.Create(client);
        }
    }
}