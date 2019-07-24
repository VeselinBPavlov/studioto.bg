namespace Studio.Application.LocationIndustries.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class DeleteLocationIndustryCommandHandler : IRequestHandler<DeleteLocationIndustryCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteLocationIndustryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteLocationIndustryCommand request, CancellationToken cancellationToken)
        {
            var locationIndustry = await this.context.LocationIndustries.FindAsync(request.LocationId, request.IndustryId);

            if (locationIndustry == null || locationIndustry.IsDeleted == true)
            {
                throw new NotFoundException(GConst.LocationIndustry, $"{request.LocationId} - {request.LocationId}");
            }            

            locationIndustry.DeletedOn = DateTime.UtcNow;
            locationIndustry.IsDeleted = true;
            
            this.context.LocationIndustries.Update(locationIndustry);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
