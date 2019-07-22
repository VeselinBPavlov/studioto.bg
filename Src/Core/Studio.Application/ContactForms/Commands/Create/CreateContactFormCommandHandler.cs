namespace Studio.Application.ContactForms.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Studio.Application.Infrastructure.SendGrid;
    using Studio.Common;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Studio.Application.Interfaces.Infrastructure;

    public class CreateContactFormCommandHandler : IRequestHandler<CreateContactFormCommand, Unit>
    {
        private readonly string apiKey = "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";
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

            emailSender.ConfigureSendGridEmailSender(loggerFactory, apiKey, GConst.SenderEmail, GConst.SenderName);
            await emailSender.SendEmailAsync(request.Email, GConst.SenderSubject, GConst.SenderMessage);

            return Unit.Value;
        }       
    }
}
