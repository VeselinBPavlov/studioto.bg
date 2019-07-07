namespace Studio.Application.Tests.Cities.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Cities.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateCityCommandHandlerTests : CommandTestBase
    {
        private int countryId;
        private Mock<IMediator> mediator;
        private CreateCityCommandHandler sut;

        public CreateCityCommandHandlerTests()
        {
            countryId = GetCountryId();
            mediator = new Mock<IMediator>();
            sut = new CreateCityCommandHandler(context, mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateCity()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCityCommand { Name = GConst.ValidName, CountryId = countryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Cities.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedCountry()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateCityCommand { Name = GConst.ValidName, CountryId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.City, GConst.ValidName, GConst.CountryLower, GConst.InvalidId), status.Message);
        }
    }
}
