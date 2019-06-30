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
            var city = new City { Name = GConst.CityValidName };
            context.Cities.Add(city);
            await this.context.SaveChangesAsync();
            var cityId = context.Cities.SingleOrDefault(x => x.Name ==  GConst.CityValidName).Id;

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
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(1, context.Addresses.Count());
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionError()
        {           
            var sut = new UpdateAddressCommandHandler(context);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, nameof(Address), GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowUpdateFailureException()
        {
            var city = new City { Name = GConst.CityValidName };
            context.Cities.Add(city);
            await this.context.SaveChangesAsync();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.CityValidName).Id;

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

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new UpdateAddressCommand { Id = addressId, Street = "Benkovski", Number = "1", CityId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, "Update", nameof(Address), "Benkovski", "city", GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowAddressFormatError()
        {
            var city = new City { Name = GConst.CityValidName };
            context.Cities.Add(city);
            this.context.SaveChanges();
            var cityId = context.Cities.SingleOrDefault(x => x.Name == GConst.CityValidName).Id;

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
            Assert.Equal(string.Format(GConst.ValueObjectExceptionMessage, nameof(Address)), status.Message);
        }
    }
}
