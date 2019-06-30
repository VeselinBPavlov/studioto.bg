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
            var country = new Country { Name = Common.GConst.CountryValidName };
            context.Countries.Add(country);
            this.context.SaveChanges();
            var countryId = context.Countries.SingleOrDefault(x => x.Name == Common.GConst.CountryValidName).Id;
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCityCommand { Name = Common.GConst.CityValidName, CountryId = countryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(Common.GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Cities.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForDeletedCountry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateCityCommand { Name = "Sofia", CountryId = Common.GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(Common.GConst.ReferenceExceptionMessage, "Creation", nameof(Domain.Entities.City), "Sofia", "country", Common.GConst.InvalidId), status.Message);
        }
    }
}
