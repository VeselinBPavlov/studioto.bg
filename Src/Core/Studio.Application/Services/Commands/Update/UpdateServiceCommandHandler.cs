namespace Studio.Application.Services.Commands.Update
{
    using Studio.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Exceptions;
    using Interfaces.Persistence;
    using System;
    using System.Linq;
    using Studio.Common;

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateServiceCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await this.context.Services
                .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (service == null)
            {
                throw new NotFoundException(nameof(Service), request.Id);
            }

            var industry = await this.context.Industries.FindAsync(request.IndustryId);

            if (industry == null)
            {
                throw new CreateFailureException(nameof(Service), request.Name, string.Format(GlobalConstants.RefereceException, "industry", request.IndustryId));
            }

            service.Name = request.Name;
            service.ModifiedOn = DateTime.UtcNow;

            this.context.Services.Update(service);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
