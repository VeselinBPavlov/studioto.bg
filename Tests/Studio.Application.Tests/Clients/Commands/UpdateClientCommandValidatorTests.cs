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
            validator.ShouldNotHaveValidationErrorFor(command => command.CompanyName, GlobalConstants.ClientValidName);
            validator.ShouldNotHaveValidationErrorFor(command => command.VatNumber, GlobalConstants.ClientValidVatNumber);
            validator.ShouldNotHaveValidationErrorFor(command => command.Phone, GlobalConstants.ClientValidPhone);
            validator.ShouldNotHaveValidationErrorFor(command => command.ManagerFirstName, GlobalConstants.ClientValidManagerFirstName);
            validator.ShouldNotHaveValidationErrorFor(command => command.ManagerLastName, GlobalConstants.ClientValidManagerLastName);
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsNull()
        {
            validator.ShouldHaveValidationErrorFor(command => command.CompanyName, null as string);
            validator.ShouldHaveValidationErrorFor(command => command.VatNumber, null as string);
            validator.ShouldHaveValidationErrorFor(command => command.Phone, null as string);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerFirstName, null as string);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerLastName, null as string);
        }

        [Fact]
        public void ShouldReturnErrorIfNameLongerThan100Characters()
        {
            validator.ShouldHaveValidationErrorFor(command => command.CompanyName, GlobalConstants.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.VatNumber, GlobalConstants.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.Phone, GlobalConstants.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerFirstName, GlobalConstants.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerLastName, GlobalConstants.InvalidName);
        }

        [Fact]
        public void ShouldReturnErrorIfNameIsEmptyString()
        {
            validator.ShouldHaveValidationErrorFor(command => command.CompanyName, string.Empty);
            validator.ShouldHaveValidationErrorFor(command => command.VatNumber, string.Empty);
            validator.ShouldHaveValidationErrorFor(command => command.Phone, string.Empty);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerFirstName, string.Empty);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerLastName, string.Empty);
        }
    }
}
