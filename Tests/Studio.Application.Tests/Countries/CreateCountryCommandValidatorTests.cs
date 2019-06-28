namespace Studio.Application.Tests.Countries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Countries.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateCountryCommandValidatorTests
    {
        private CreateCountryCommandValidator createValidator;
        private CreateCountryCommand createCommand;

        public CreateCountryCommandValidatorTests()
        {
            this.createValidator = new CreateCountryCommandValidator();
            this.createCommand = new CreateCountryCommand();
        }

        [Fact]
        public void CountryShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Name, GlobalConstants.CountryValidName);
        }

        [Fact]
        public void CountryShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, null as string);
        }

        [Fact]
        public void CountryShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, GlobalConstants.InvalidName);
        }

        [Fact]
        public void CountryShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, string.Empty);
        }
    }
}
