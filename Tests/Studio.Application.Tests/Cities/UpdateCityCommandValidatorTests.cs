namespace Studio.Application.Tests.Cities.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Cities.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateCityCommandValidatorTests
    {
        private UpdateCityCommandValidator updateValidator;
        private UpdateCityCommand updateCommand;

        public UpdateCityCommandValidatorTests()
        {
            this.updateValidator = new UpdateCityCommandValidator();
            this.updateCommand = new UpdateCityCommand();
        }

        [Fact]
        public void CityShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Name, GlobalConstants.CityValidName);
        }

        [Fact]
        public void CityShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, null as string);
        }

        [Fact]
        public void CityShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, GlobalConstants.InvalidName);
        }

        [Fact]
        public void CityShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, string.Empty);
        }
    }
}
