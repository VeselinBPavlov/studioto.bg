namespace Studio.Application.Tests.Services.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Services.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateServiceCommandValidatorTests
    {
        private CreateServiceCommandValidator createValidator;
        private CreateServiceCommand createCommand;

        public CreateServiceCommandValidatorTests()
        {
            this.createValidator = new CreateServiceCommandValidator();
            this.createCommand = new CreateServiceCommand();
        }

        [Fact]
        public void ServiceShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Name, GConst.ValidName);
        }

        [Fact]
        public void ServiceShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, null as string);
        }

        [Fact]
        public void ServiceShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, GConst.InvalidName);
        }

        [Fact]
        public void ServiceShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, string.Empty);
        }
    }
}
