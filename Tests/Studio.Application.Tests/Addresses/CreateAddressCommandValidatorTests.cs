namespace Studio.Application.Tests.Addresses.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Addresses.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateAddressCommandValidatorTests
    {
        private CreateAddressCommandValidator createValidator;
        private CreateAddressCommand createCommand;

        public CreateAddressCommandValidatorTests()
        {
            this.createValidator = new CreateAddressCommandValidator();
            this.createCommand = new CreateAddressCommand();
        }

        [Fact]
        public void AddressShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Street, "Benkovski");
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Number, "1");
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Building, "1");
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Entrance, "A");
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Floor, "1");
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Longitude, 40.00M);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Latitude, -40.00M);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.CityId, 1);
        }

        [Fact]
        public void AddressShouldReturnErrorIfStreetOrNumberIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Street, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Number, null as string);
        }

        [Fact]
        public void AddressShouldReturnErrorIfStreetOrDistinctLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Street, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.District, GConst.InvalidName);
        }

        [Fact]
        public void AddressShouldReturnErrorIfStreetOrNumberIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Street, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Number, string.Empty);
        }

        [Fact]
        public void AddressShouldReturnErrorIfNumberBuildingEntranceFloorApartmentAreLongerThan10Chars()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Number, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Building, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Entrance, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Floor, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Apartment, GConst.InvalidName);
        }
    }
}
