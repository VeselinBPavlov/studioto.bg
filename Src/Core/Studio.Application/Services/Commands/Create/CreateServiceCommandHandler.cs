namespace Studio.Application.Services.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System.Linq;
    using Studio.Application.Exceptions;
    using System;
    using Studio.Common;

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateServiceCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var industry = await this.context.Industries.FindAsync(request.IndustryId);

            if (industry == null || industry.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Service, request.Name, string.Format(GConst.RefereceException, GConst.IndustryLower, request.IndustryId));
            }

            var service = new Service
            {
                Name = request.Name,
                IndustryId = request.IndustryId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Services.Add(service);

            await this.context.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new CreateServiceCommandNotification { ServiceId = service.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
