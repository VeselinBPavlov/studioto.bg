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
        [Fact]
        public async Task ShouldCreateIndustry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateIndustryCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateIndustryCommand { Name = Common.GConst.IndustryValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(Common.GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Industries.Count());
        }
    }
}
