namespace Studio.Application.Tests.Industries.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;

    public class CreateIndustryCommandHandlerTests : CommandTestBase
    {
        private Mock<IMediator> mediator;
        private CreateIndustryCommandHandler sut;

        public CreateIndustryCommandHandlerTests()
        {
            mediator = new Mock<IMediator>();
            sut = new CreateIndustryCommandHandler(context, mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateIndustry()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateIndustryCommand { Name = GConst.ValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Industries.Count());
        }
    }
}
