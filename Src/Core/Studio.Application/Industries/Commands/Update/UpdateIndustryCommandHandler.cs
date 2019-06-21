namespace Studio.Application.Industries.Commands.Update
{
    using Studio.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Exceptions;
    using Interfaces.Persistence;

    public class UpdateIndustryCommandHandler : IRequestHandler<UpdateIndustryCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateIndustryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateIndustryCommand request, CancellationToken cancellationToken)
        {
            var industry = await this.context.Industries
                .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (industry == null)
            {
                // TODO: Add functional test for this behaviour.
                throw new NotFoundException(nameof(Industry), request.Id);
            }

            industry.Name = request.Name;

            this.context.Industries.Update(industry);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
