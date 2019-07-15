namespace Studio.Application.Locations.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteLocationCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await context.Locations.FindAsync(request.Id);

            if (location == null || location.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Location, request.Id);
            }

            var hasEmployees = this.context.Employees.Any(e => e.LocationId == location.Id && e.Location.IsDeleted == false);

            if (hasEmployees)
            {
                throw new DeleteFailureException(Common.GConst.Location, request.Id, string.Format(GConst.DeleteException, GConst.Employees, GConst.LocationLower));
            }   

            var hasIndustries = this.context.LocationIndustries.Any(e => e.LocationId == location.Id && e.Location.IsDeleted == false);

            if (hasIndustries)
            {
                throw new DeleteFailureException(Common.GConst.Location, request.Id, string.Format(GConst.DeleteException, GConst.Industries, GConst.LocationLower));
            }

            location.Address.DeletedOn = DateTime.UtcNow;
            location.Address.IsDeleted = true;

            location.DeletedOn = DateTime.UtcNow;
            location.IsDeleted = true;
            
            this.context.Locations.Update(location);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
