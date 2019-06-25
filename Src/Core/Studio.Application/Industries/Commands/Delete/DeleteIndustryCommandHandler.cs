﻿namespace Studio.Application.Industries.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteIndustryCommandHandler : IRequestHandler<DeleteIndustryCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteIndustryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteIndustryCommand request, CancellationToken cancellationToken)
        {
            var industry = await context.Industries
                .FindAsync(request.Id);

            if (industry == null)
            {
                throw new NotFoundException(nameof(Industry), request.Id);
            }

            var hasServices = this.context.Services.Any(s => s.IndustryId == industry.Id);

            if (hasServices)
            {
                throw new DeleteFailureException(nameof(Service), request.Id, "There are existing services associated with this industry.");
            }

            this.context.Industries.Remove(industry);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}