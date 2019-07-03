namespace Studio.Application.Tests.ContactForms.Commands
{
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.ContactForms.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;

    public class CreateContactFormCommandNotificationTests : CommandTestBase
    {
        [Fact]
        public void ShouldRaiseContactFormCreatedNotification()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateContactFormCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateContactFormCommand { FirstName = GConst.ValidName}, CancellationToken.None);
            var ContactFormId = context.ContactForms.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateContactFormCommandNotification>(c => c.ContactFormId == ContactFormId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}