namespace Studio.Application.Tests.Employees.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Employees.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateEmployeeCommandValidatorTests
    {
        private CreateEmployeeCommandValidator createValidator;
        private CreateEmployeeCommand createCommand;

        public CreateEmployeeCommandValidatorTests()
        {
            this.createValidator = new CreateEmployeeCommandValidator();
            this.createCommand = new CreateEmployeeCommand();
        }

        [Fact]
        public void EmployeeShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.FirstName, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.LastName, GConst.ValidName);
        }

        [Fact]
        public void EmployeeShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, null as string);
        }

        [Fact]
        public void EmployeeShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, GConst.InvalidName);
        }

        [Fact]
        public void EmployeeShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, string.Empty);
        }
    }
}
