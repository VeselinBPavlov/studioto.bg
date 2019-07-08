namespace Studio.Application.Tests.Locations.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Locations.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateLocationCommandHandlerTests : CommandTestBase
    {
        private int clientId;
        private int addressId;
        private Mock<IMediator> mediator;
        private CreateLocationCommandHandler sut;

        public CreateLocationCommandHandlerTests()
        {
            clientId = GetClientId();
            addressId = GetAddressId(null);
            mediator = new Mock<IMediator>();
            sut = new CreateLocationCommandHandler(context, mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateLocation()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateLocationCommand 
            { 
                Name = GConst.ValidName, 
                StartDay = GConst.ValidStartDay,
                EndDay = GConst.ValidEndDay,
                ClientId = clientId, 
                AddressId = addressId 
            }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Locations.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidClient()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateLocationCommand { Name = GConst.ValidName, ClientId = GConst.InvalidId, AddressId = addressId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Location, GConst.ValidName, GConst.ClientLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidAddress()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateLocationCommand { Name = GConst.ValidName, ClientId = clientId, AddressId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Location, GConst.ValidName, GConst.AddressLower, GConst.InvalidId), status.Message);
        }
    }
}
