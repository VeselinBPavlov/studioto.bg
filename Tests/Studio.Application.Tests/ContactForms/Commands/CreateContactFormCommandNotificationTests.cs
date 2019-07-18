namespace Studio.Application.Tests.ContactForms.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Studio.Application.ContactForms.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    public class CreateContactFormCommandNotificationTests : CommandTestBase
    {
        private Mock<IMediator> mediator;
        private CreateContactFormCommandHandler sut;
        private ILoggerFactory loggerFactory;

        public CreateContactFormCommandNotificationTests()
        {
            mediator = new Mock<IMediator>();
            loggerFactory = new LoggerFactory();
            sut = new CreateContactFormCommandHandler(context, mediator.Object, loggerFactory);
        }

        [Fact]
        public void ShouldRaiseContactFormCreatedNotification()
        {
            var result = sut.Handle(new CreateContactFormCommand {  FirstName = GConst.ValidName, LastName = GConst.ValidName, Topic = GConst.ValidName, Message = GConst.ValidName, Email = "vp_fin@abv.bg" }, CancellationToken.None);
            var ContactFormId = context.ContactForms.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateContactFormCommandNotification>(c => c.ContactFormId == ContactFormId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}