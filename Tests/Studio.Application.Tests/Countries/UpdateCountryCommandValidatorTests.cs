namespace Studio.Application.Tests.Countries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Countries.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateCountryCommandValidatorTests
    {
        private UpdateCountryCommandValidator updateValidator;
        private UpdateCountryCommand updateCommand;

        public UpdateCountryCommandValidatorTests()
        {
            this.updateValidator = new UpdateCountryCommandValidator();
            this.updateCommand = new UpdateCountryCommand();
        }

        [Fact]
        public void CountryShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Name, GlobalConstants.CountryValidName);
        }

        [Fact]
        public void CountryShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, null as string);
        }

        [Fact]
        public void CountryShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, GlobalConstants.InvalidName);
        }

        [Fact]
        public void CountryShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, string.Empty);
        }
    }
}
