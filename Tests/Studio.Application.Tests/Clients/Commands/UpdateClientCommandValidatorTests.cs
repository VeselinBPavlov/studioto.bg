namespace Studio.Application.Tests.Clients.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Clients.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateClientCommandValidatorTests
    {
        private UpdateClientCommandValidator validator;
        private UpdateClientCommand command;

        public UpdateClientCommandValidatorTests()
        {
            validator = new UpdateClientCommandValidator();
            command = new UpdateClientCommand();
        }

        [Fact]
        public void ShouldNotReturnError()
        {
            validator.ShouldNotHaveValidationErrorFor(command => command.CompanyName, GConst.ClientValidName);
            validator.ShouldNotHaveValidationErrorFor(command => command.VatNumber, GConst.ValidVatNumber);
            validator.ShouldNotHaveValidationErrorFor(command => command.Phone, GConst.ValidPhone);
            validator.ShouldNotHaveValidationErrorFor(command => command.ManagerFirstName, GConst.ClientValidManagerFirstName);
            validator.ShouldNotHaveValidationErrorFor(command => command.ManagerLastName, GConst.ClientValidManagerLastName);
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
            validator.ShouldHaveValidationErrorFor(command => command.CompanyName, GConst.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.VatNumber, GConst.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.Phone, GConst.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerFirstName, GConst.InvalidName);
            validator.ShouldHaveValidationErrorFor(command => command.ManagerLastName, GConst.InvalidName);
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
