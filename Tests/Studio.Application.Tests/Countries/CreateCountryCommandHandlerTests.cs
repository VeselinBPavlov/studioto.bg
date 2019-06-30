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
        [Fact]
        public async Task ShouldCreateCountry()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateCountryCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateCountryCommand { Name = Common.GConst.CountryValidName }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(Common.GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Countries.Count());
        }

        [Fact]
        public async Task ShouldThrowCreateFailureException()
        {
             var country = new Country { Name = "Beauty" };

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var countryId = context.Countries.SingleOrDefault(x => x.Name == "Beauty").Id;
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateCountryCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateCountryCommand { Name = "Beauty" }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(Common.GConst.UniqueNameExceptionMessage, "Creation", nameof(Domain.Entities.Country),  "Beauty", "country"), status.Message);
        }
    }
}
