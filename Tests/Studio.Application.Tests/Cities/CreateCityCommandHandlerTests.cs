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
        [Fact]
        public async Task ShouldCreateCity()
        {
            var countryId = GetCountryId();
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCityCommand { Name = GConst.ValidName, CountryId = countryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Cities.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedCountry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateCityCommand { Name = GConst.ValidName, CountryId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.City, GConst.ValidName, GConst.CountryLower, GConst.InvalidId), status.Message);
        }
    }
}
