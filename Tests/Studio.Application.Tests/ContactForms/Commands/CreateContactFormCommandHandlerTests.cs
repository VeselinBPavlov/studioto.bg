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
    using Microsoft.Extensions.Logging;

    public class CreateContactFormCommandHandlerTests : CommandTestBase
    {
        private Mock<IMediator> mediator;
        private CreateContactFormCommandHandler sut;
        private ILoggerFactory loggerFactory;

        public CreateContactFormCommandHandlerTests()
        {
            mediator = new Mock<IMediator>();
            loggerFactory = new LoggerFactory();
            sut = new CreateContactFormCommandHandler(context, mediator.Object, loggerFactory);            
        }

        [Fact]
        public async Task ShouldCreateContactForm()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateContactFormCommand { FirstName = GConst.ValidName, LastName = GConst.ValidName, Topic = GConst.ValidName, Message = GConst.ValidName, Email = "vp_fin@abv.bg" }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.ContactForms.Count());
        }
    }
}
