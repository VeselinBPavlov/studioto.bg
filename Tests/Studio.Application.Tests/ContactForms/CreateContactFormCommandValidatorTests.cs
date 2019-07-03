namespace Studio.Application.Tests.ContactForms.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.ContactForms.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateContactFormCommandValidatorTests
    {
        private CreateContactFormCommandValidator createValidator;
        private CreateContactFormCommand createCommand;

        public CreateContactFormCommandValidatorTests()
        {
            this.createValidator = new CreateContactFormCommandValidator();
            this.createCommand = new CreateContactFormCommand();
        }

        [Fact]
        public void ContactFormShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.FirstName, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.LastName, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Email, GConst.ValidEmail);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Topic, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Message, GConst.ValidName);
        }

        [Fact]
        public void ContactFormShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Topic, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Message, null as string);
        }

        [Fact]
        public void ContactFormShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Topic, GConst.InvalidName);
        }

        [Fact]
        public void ContactFormShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Topic, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Message, string.Empty);
        }

        [Fact]
        public void ContactFormShouldReturnErrorEmailIsInvalid()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, GConst.ValidName);
        }
    }
}
