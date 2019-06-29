namespace Studio.Application.Cities.Commands.Update
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

    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateCityCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await this.context.Cities
                .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (city == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }
            
            city.Name = request.Name;
            city.ModifiedOn = DateTime.UtcNow;

            this.context.Cities.Update(city);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
