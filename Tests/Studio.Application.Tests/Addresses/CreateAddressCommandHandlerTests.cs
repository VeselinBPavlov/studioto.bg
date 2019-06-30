namespace Studio.Application.Tests.Addresses.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Addresses.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateAddressCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateAddress()
        {
            var city = new City { Name = Common.GConst.FirstValidName };
            context.Cities.Add(city);
            this.context.SaveChanges();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == Common.GConst.FirstValidName).Id;
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateAddressCommand { Street = Common.GConst.FirstValidName, Number = Common.GConst.ValidAddressNumber, CityId = cityId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(Common.GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Addresses.Count());
        }

        [Fact]
        public async Task ShouldThrowAddressFormatError()
        {
            var city = new City { Name = Common.GConst.CityValidName };
            context.Cities.Add(city);
            this.context.SaveChanges();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == Common.GConst.CityValidName).Id;

            var mediator = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAddressCommand { CityId = cityId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(Common.GConst.ValueObjectExceptionMessage, nameof(Domain.Entities.Address)), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureException()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAddressCommand { Street = "Ropotamo", Number = "12", CityId = Common.GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(Common.GConst.ReferenceExceptionMessage, "Creation", nameof(Domain.Entities.Address), "Ropotamo", "city", Common.GConst.InvalidId), status.Message);
        }
    }
}
