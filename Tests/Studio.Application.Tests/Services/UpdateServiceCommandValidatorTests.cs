namespace Studio.Application.Tests.Services.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Services.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateServiceCommandValidatorTests
    {
        private UpdateServiceCommandValidator updateValidator;
        private UpdateServiceCommand updateCommand;

        public UpdateServiceCommandValidatorTests()
        {
            this.updateValidator = new UpdateServiceCommandValidator();
            this.updateCommand = new UpdateServiceCommand();
        }

        [Fact]
        public void ServiceShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Name, GConst.ValidName);
        }

        [Fact]
        public void ServiceShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, null as string);
        }

        [Fact]
        public void ServiceShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, GConst.InvalidName);
        }

        [Fact]
        public void ServiceShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, string.Empty);
        }
    }
}
