namespace Studio.Application.Industries.Commands.Update
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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
                .SingleOrDefaultAsync(i => i.Id == request.Id && i.IsDeleted != true, cancellationToken);

            if (industry == null)
            {
                throw new NotFoundException(GConst.Industry, request.Id);
            }

            industry.Name = request.Name;
            industry.Possition = request.Possition;
            industry.ModifiedOn = DateTime.UtcNow;

            this.context.Industries.Update(industry);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
