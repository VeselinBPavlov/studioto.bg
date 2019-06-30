namespace Studio.Application.Tests.Addresses.Commands
{
    using MediatR;
    using Studio.Application.Addresses.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Domain.ValueObjects;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateAddressCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldUpdateAddress()
        {
            var city = new City { Name = GlobalConstants.CityValidName };
            context.Cities.Add(city);
            await this.context.SaveChangesAsync();
            var cityId = context.Cities.SingleOrDefault(x => x.Name ==  GlobalConstants.CityValidName).Id;

            var inputAddressData = new InputAddressData
            {
                Street = "Iskar",
                Number = "13"
            };

            var address = new Address { AddressFormat = AddressFormat.For(inputAddressData), CityId = cityId };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var addressId = context.Addresses.SingleOrDefault(x => x.CityId == cityId).Id;

            var sut = new UpdateAddressCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new UpdateAddressCommand { Id = addressId, Street = "Benkovski", Number = "1", CityId = cityId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GlobalConstants.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Addresses.Count());
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionError()
        {           
            var sut = new UpdateAddressCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = GlobalConstants.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.NotFoundExceptionMessage, nameof(Address), GlobalConstants.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowUpdateFailureException()
        {
            var city = new City { Name = GlobalConstants.CityValidName };
            context.Cities.Add(city);
            await this.context.SaveChangesAsync();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == GlobalConstants.CityValidName).Id;

            var inputAddressData = new InputAddressData
            {
                Street = "Iskar",
                Number = "13"
            };

            var address = new Address { AddressFormat = AddressFormat.For(inputAddressData), CityId = cityId };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var addressId = context.Addresses.SingleOrDefault(x => x.CityId == cityId).Id;

            var sut = new UpdateAddressCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = addressId, Street = "Benkovski", Number = "1", CityId = GlobalConstants.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GlobalConstants.UpdateFailureExceptionMessage, nameof(Address), addressId, "city", GlobalConstants.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowAddressFormatError()
        {
            var city = new City { Name = GlobalConstants.CityValidName };
            context.Cities.Add(city);
            this.context.SaveChanges();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == GlobalConstants.CityValidName).Id;

            var inputAddressData = new InputAddressData
            {
                Street = "Iskar",
                Number = "13"
            };

            var address = new Address { AddressFormat = AddressFormat.For(inputAddressData), CityId = cityId };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var addressId = context.Addresses.SingleOrDefault(x => x.CityId == cityId).Id;

            var sut = new UpdateAddressCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = addressId, CityId = cityId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(GlobalConstants.AddressFromatExceptionMessage, status.Message);
        }
    }
}
