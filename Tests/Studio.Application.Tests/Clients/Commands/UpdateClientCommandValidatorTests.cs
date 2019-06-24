namespace Studio.Application.Tests.Industries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Industries.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateClientCommandValidatorTests
    {
        private UpdateClientCommandValidator validator;
        private UpdateClientCommand command;

        public UpdateClientCommandValidatorTests()
        {
            this.validator = new UpdateClientCommandValidator();
            this.command = new UpdateClientCommand();
        }

        [Fact]
        public void ShouldNotReturnError()
        {
            validator.ShouldNotHaveValidationErrorFor(command => command.Name, GlobalConstants.IndustryValidName);
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
