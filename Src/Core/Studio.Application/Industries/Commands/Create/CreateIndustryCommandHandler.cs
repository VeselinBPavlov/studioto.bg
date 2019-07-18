namespace Studio.Application.Industries.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Interfaces.Persistence;
    using MediatR;

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
                Possition = request.Possition,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Industries.Add(industry);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
