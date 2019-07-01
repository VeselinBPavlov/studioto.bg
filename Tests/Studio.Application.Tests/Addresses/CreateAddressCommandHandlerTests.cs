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
            int cityId = GetCityId(null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateAddressCommand { Street = GConst.ValidName, Number = GConst.ValidAddressNumber, CityId = cityId }, CancellationToken.None));

            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Addresses.Count());
        }        

        [Fact]
        public async Task ShouldThrowAddressFormatError()
        {
            var cityId = GetCityId(null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAddressCommand { CityId = cityId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ValueObjectExceptionMessage, GConst.Address), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureException()
        {
            var mediator = new Mock<IMediator>();
            var sut = new CreateAddressCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAddressCommand { Street = GConst.ValidName, Number = GConst.ValidAddressNumber, CityId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Address, GConst.ValidName, GConst.CityLower, GConst.InvalidId), status.Message);
        }
    }
}
