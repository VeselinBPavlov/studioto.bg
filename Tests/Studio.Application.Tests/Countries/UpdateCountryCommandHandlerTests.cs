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
        [Fact]
        public async void CountryShouldUpdateCorrect()
        {
            var country = new Country { Name = GlobalConstants.CountryValidName };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == GlobalConstants.CountryValidName).Id;

            var sut = new UpdateCountryCommandHandler(context);
            var updatedCountry = new UpdateCountryCommand { Id = countryId, Name = "Mars" };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedCountry, CancellationToken.None));

            var resultId = context.Countries.SingleOrDefault(x => x.Name == "Mars").Id;

            Assert.Equal(countryId, resultId);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Countries.Count());
        }

        [Fact]
        public async void CountryShouldThrowNotFoundException()
        {
            var sut = new UpdateCountryCommandHandler(context);
            var updatedCountry = new UpdateCountryCommand { Id = GlobalConstants.InvalidId, Name = GlobalConstants.CountrySecondValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedCountry, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.NotFoundExceptionMessage, nameof(Country), GlobalConstants.InvalidId), status.Message);            
        }
    }
}
