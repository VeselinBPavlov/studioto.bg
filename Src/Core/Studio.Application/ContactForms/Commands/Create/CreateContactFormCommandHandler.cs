namespace Studio.Application.ContactForms.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Entities;
    using Interfaces.Infrastructure;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CreateContactFormCommandHandler : IRequestHandler<CreateContactFormCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;
        private readonly ILoggerFactory loggerFactory;
        private readonly ISender emailSender;

        public CreateContactFormCommandHandler(IStudioDbContext context, IMediator mediator, ILoggerFactory loggerFactory, ISender emailSender)
        {
            this.loggerFactory = loggerFactory;
            this.emailSender = emailSender;
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

            this.emailSender.ConfigureSendGridEmailSender(this.loggerFactory, GConst.ApiKey, GConst.SenderEmail, GConst.SenderName);
            await this.emailSender.SendEmailAsync(request.Email, GConst.SenderSubject, GConst.SenderMessage);

            return Unit.Value;
        }       
    }
}
