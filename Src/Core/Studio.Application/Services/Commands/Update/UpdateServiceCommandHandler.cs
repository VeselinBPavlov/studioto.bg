namespace Studio.Application.Services.Commands.Update
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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
                .SingleOrDefaultAsync(s => s.Id == request.Id && s.IsDeleted != true, cancellationToken);

            if (service == null)
            {
                throw new NotFoundException(GConst.Service, request.Id);
            }

            var industry = await this.context.Industries.FindAsync(request.IndustryId);

            if (industry == null || industry.IsDeleted == true)
            {
                throw new UpdateFailureException(GConst.Service, request.Id, string.Format(GConst.RefereceException, GConst.IndustryLower, request.IndustryId));
            }

            service.Name = request.Name;
            service.ModifiedOn = DateTime.UtcNow;

            this.context.Services.Update(service);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
