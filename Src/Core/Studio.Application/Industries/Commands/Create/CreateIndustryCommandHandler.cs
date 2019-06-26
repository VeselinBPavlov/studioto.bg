namespace Studio.Application.Industries.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;

    public class CreateIndustryCommandHandler : IRequestHandler<CreateIndustryCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateIndustryCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateIndustryCommand request, CancellationToken cancellationToken)
        {
            var industry = new Industry
            {
                Name = request.Name,
                Possition = request.Possition
            };

            context.Industries.Add(industry);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateIndustryCommandNotification { IndustryId = industry.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
