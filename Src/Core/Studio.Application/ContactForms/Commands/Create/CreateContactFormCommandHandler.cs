namespace Studio.Application.ContactForms.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System;

    public class CreateContactFormCommandHandler : IRequestHandler<CreateContactFormCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateContactFormCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateContactFormCommand request, CancellationToken cancellationToken)
        {
            var contactForm = new ContactForm
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Topic = request.Topic,
                Message = request.Message,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.ContactForms.Add(contactForm);

            await this.context.SaveChangesAsync(cancellationToken);

            // TODO: Send email to admin.

            await this.mediator.Publish(new CreateContactFormCommandNotification { ContactFormId = contactForm.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
