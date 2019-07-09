namespace Studio.Application.Tests.ContactForms.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.ContactForms.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;

    public class CreateContactFormCommandHandlerTests : CommandTestBase
    {
        private Mock<IMediator> mediator;
        private CreateContactFormCommandHandler sut;

        public CreateContactFormCommandHandlerTests()
        {
            mediator = new Mock<IMediator>();
            sut = new CreateContactFormCommandHandler(context, mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateContactForm()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateContactFormCommand { FirstName = GConst.ValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.ContactForms.Count());
        }
    }
}
