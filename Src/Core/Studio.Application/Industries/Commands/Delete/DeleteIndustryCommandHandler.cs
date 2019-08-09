namespace Studio.Application.Industries.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class DeleteIndustryCommandHandler : IRequestHandler<DeleteIndustryCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteIndustryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteIndustryCommand request, CancellationToken cancellationToken)
        {
            var industry = await this.context.Industries
                .FindAsync(request.Id);

            if (industry == null || industry.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Industry, request.Id);
            }

            var hasServices = this.context.Services.Where(s => s.IsDeleted != true).Any(s => s.IndustryId == industry.Id && s.Industry.IsDeleted == false);

            if (hasServices)
            {
                throw new DeleteFailureException(GConst.Industry, request.Id, string.Format(GConst.DeleteException, GConst.Services, GConst.IndustryLower));
            }

            var hasLocations = this.context.LocationIndustries.Any(li => li.IndustryId == industry.Id && li.Industry.IsDeleted == false);

            if (hasLocations)
            {
                throw new DeleteFailureException(GConst.Industry, request.Id, string.Format(GConst.DeleteException, GConst.Locations, GConst.IndustryLower));
            }

            industry.DeletedOn = DateTime.UtcNow;
            industry.IsDeleted = true;
            
            this.context.Industries.Update(industry);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
