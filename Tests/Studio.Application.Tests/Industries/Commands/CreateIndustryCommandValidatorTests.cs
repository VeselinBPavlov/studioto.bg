namespace Studio.Application.Tests.Industries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateIndustryCommandValidatorTests
    {
        private CreateIndustryCommandValidator validator;
        private CreateIndustryCommand command;

        public CreateIndustryCommandValidatorTests()
        {
            this.validator = new CreateIndustryCommandValidator();
            this.command = new CreateIndustryCommand();
        }

        [Fact]
        public void ShouldNotReturnError()
        {
            validator.ShouldNotHaveValidationErrorFor(command => command.Name, GlobalConstants.IndustryValidName);
            validator.ShouldNotHaveValidationErrorFor(command => command.Possition, GlobalConstants.IndustryPossition);
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsNull()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, null as string);
            validator.ShouldHaveValidationErrorFor(command => command.Possition, null as string);
        }

        [Fact]
        public void ShouldReturnErrorIfNameLongerThan100Characters()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, GlobalConstants.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.Possition, GlobalConstants.InvalidName);
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsEmptyString()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, string.Empty);
            validator.ShouldHaveValidationErrorFor(command => command.Possition, string.Empty);
        }
    }
}
