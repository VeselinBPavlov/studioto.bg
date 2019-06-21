namespace Studio.Application.Tests.Industries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Industries.Commands.Create;
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
            validator.ShouldNotHaveValidationErrorFor(command => command.Name, "Hairstyle");
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsNull()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, null as string);
        }

        [Fact]
        public void ShouldReturnErrorIfNameLongerThan100Characters()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, new string('A', 101));
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsEmptyString()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, string.Empty);
        }
    }
}
