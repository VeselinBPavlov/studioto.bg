namespace Studio.Application.Tests.Countries.Commands
{
    using MediatR;
    using Studio.Application.Countries.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateCountryCommandHandlerTests : CommandTestBase
    {
        private int countryId;
        private UpdateCountryCommandHandler sut;

        public UpdateCountryCommandHandlerTests()
        {
            countryId = CommandArrangeHelper.GetCountryId(context);
            sut = new UpdateCountryCommandHandler(context);
        }

        [Fact]
        public async void CountryShouldUpdateCorrect()
        {
            var updatedCountry = new UpdateCountryCommand { Id = countryId, Name = GConst.UpdatedName };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedCountry, CancellationToken.None));

            var resultId = context.Countries.SingleOrDefault(x => x.Name == GConst.UpdatedName).Id;

            Assert.Equal(countryId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Countries.Count());
        }

        [Fact]
        public async void CountryShouldThrowNotFoundException()
        {
            var updatedCountry = new UpdateCountryCommand { Id = GConst.InvalidId, Name = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCountry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Country, GConst.InvalidId), status.Message);            
        }
    }
}
