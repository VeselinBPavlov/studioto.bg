namespace Studio.Application.Tests.Countries.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Countries.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteCountryCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteCountry()
        {
            var countryId = GetCountryId();

            var sut = new DeleteCountryCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteCountryCommand { Id = countryId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }

        [Fact]
        public async Task CountryShouldТhrowDeleteFailureException()
        {
            var countryId = GetCountryId();

            GetCityId(countryId);

            var sut = new DeleteCountryCommandHandler(context);
            
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCountryCommand { Id = countryId }, CancellationToken.None));
            
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.DeleteFailureExceptionMessage, GConst.Country, countryId, GConst.Cities, GConst.CountryLower), status.Message);
        }
        

        [Fact]
        public async Task CountryShouldТhrowNotFoundException()
        {
            var sut = new DeleteCountryCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteCountryCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Country, GConst.InvalidId), status.Message);
        }
    }
}
