namespace Studio.Application.Tests.Cities.Commands
{
    using MediatR;
    using Studio.Application.Cities.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateCityCommandHandlerTests : CommandTestBase
    {
        private int countryId;
        private int cityId;
        private UpdateCityCommandHandler sut;

        public UpdateCityCommandHandlerTests()
        {
            countryId = CommandArrangeHelper.GetCountryId(context);
            cityId = CommandArrangeHelper.GetCityId(context, countryId);
            sut = new UpdateCityCommandHandler(context);
        }

        [Fact]
        public async void CityShouldUpdateCorrect()
        {
            var updatedCity = new UpdateCityCommand { Id = cityId, Name = GConst.ValidName, CountryId = countryId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedCity, CancellationToken.None));

            var resultId = context.Cities.SingleOrDefault(x => x.Name == GConst.ValidName).Id;

            Assert.Equal(cityId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Cities.Count());
        }

        [Fact]
        public async void CityShouldThrowReferenceException()
        {
            var updatedCity = new UpdateCityCommand { Id = cityId, Name = GConst.ValidName, CountryId = GConst.InvalidId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCity, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.City, cityId, GConst.CountryLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void CityShouldThrowNotFoundException()
        {
            var updatedCity = new UpdateCityCommand { Id = GConst.InvalidId, Name = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCity, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.City, GConst.InvalidId), status.Message);            
        }
    }
}
