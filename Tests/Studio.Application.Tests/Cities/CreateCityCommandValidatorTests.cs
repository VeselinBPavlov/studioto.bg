namespace Studio.Application.Tests.Cities.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Cities.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateCityCommandValidatorTests
    {
        private CreateCityCommandValidator createValidator;
        private CreateCityCommand createCommand;

        public CreateCityCommandValidatorTests()
        {
            this.createValidator = new CreateCityCommandValidator();
            this.createCommand = new CreateCityCommand();
        }

        [Fact]
        public void CityShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Name, GlobalConstants.CityValidName);
        }

        [Fact]
        public void CityShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, null as string);
        }

        [Fact]
        public void CityShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, GlobalConstants.InvalidName);
        }

        [Fact]
        public void CityShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, string.Empty);
        }
    }
}
