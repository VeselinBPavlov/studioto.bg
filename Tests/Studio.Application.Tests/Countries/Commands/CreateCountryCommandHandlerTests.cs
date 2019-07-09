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
    using Studio.Domain.Entities;

    public class CreateCountryCommandHandlerTests : CommandTestBase
    {
        private Mock<IMediator> mediator;
        private CreateCountryCommandHandler sut;

        public CreateCountryCommandHandlerTests()
        {
            mediator = new Mock<IMediator>();
            sut = new CreateCountryCommandHandler(context, mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateCountry()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCountryCommand { Name = GConst.ValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Countries.Count());
        }

        [Fact]
        public async Task ShouldThrowCreateFailureException()
        {
            var countryId = CommandArrangeHelper.GetCountryId(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateCountryCommand { Name = GConst.ValidName }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.UniqueNameExceptionMessage, GConst.Create, GConst.Country,  GConst.ValidName, GConst.CountryLower), status.Message);
        }
    }
}
