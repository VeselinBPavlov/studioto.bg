namespace Studio.Application.Tests.Countries.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;

    public class CreateCountryCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateCountry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateCountryCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCountryCommand { Name = GlobalConstants.CountryValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Countries.Count());
        }
    }
}
