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
            var country = new Country { Name = GlobalConstants.CountryValidName };
            context.Countries.Add(country);
            this.context.SaveChanges();
            var countryId = context.Countries.SingleOrDefault(x => x.Name ==  GlobalConstants.CountryValidName).Id;
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateCityCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCityCommand { Name = GlobalConstants.CityValidName, CountryId = countryId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Cities.Count());
        }
    }
}
