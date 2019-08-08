namespace Studio.Application.Cities.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteCityCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await this.context.Cities.FindAsync(request.Id);

            if (city == null || city.IsDeleted == true)
            {
                throw new NotFoundException(GConst.City, request.Id);
            }

            var hasAddresses = this.context.Addresses.Where(a => a.IsDeleted != true).Any(a => a.CityId == city.Id && a.City.IsDeleted == false);

            if (hasAddresses)
            {
                throw new DeleteFailureException(Common.GConst.City, request.Id, string.Format(GConst.DeleteException, GConst.Addresses, GConst.CityLower));
            }            

            city.DeletedOn = DateTime.UtcNow;
            city.IsDeleted = true;
            
            this.context.Cities.Update(city);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
