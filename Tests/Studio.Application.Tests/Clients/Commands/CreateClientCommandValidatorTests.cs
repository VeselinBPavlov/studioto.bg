namespace Studio.Application.Tests.Industries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateClientCommandValidatorTests
    {
        private CreateClientCommandValidator validator;
        private CreateClientCommand command;

        public CreateClientCommandValidatorTests()
        {
            this.validator = new CreateClientCommandValidator();
            this.command = new CreateClientCommand();
        }

        [Fact]
        public void ShouldNotReturnError()
        {
            validator.ShouldNotHaveValidationErrorFor(command => command.Name, GlobalConstants.ClientValidName);
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsNull()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, null as string);
        }

        [Fact]
        public void ShouldReturnErrorIfNameLongerThan100Characters()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, GlobalConstants.InvalidName);
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsEmptyString()
        {
            validator.ShouldHaveValidationErrorFor(command => command.Name, string.Empty);
        }
    }
}
