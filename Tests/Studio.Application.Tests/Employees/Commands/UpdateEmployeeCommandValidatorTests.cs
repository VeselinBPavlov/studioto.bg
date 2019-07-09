namespace Studio.Application.Tests.Employees.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Employees.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateEmployeeCommandValidatorTests
    {
        private UpdateEmployeeCommandValidator updateValidator;
        private UpdateEmployeeCommand updateCommand;

        public UpdateEmployeeCommandValidatorTests()
        {
            this.updateValidator = new UpdateEmployeeCommandValidator();
            this.updateCommand = new UpdateEmployeeCommand();
        }

        [Fact]
        public void EmployeeShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.FirstName, GConst.ValidName);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.LastName, GConst.ValidName);
        }

        [Fact]
        public void EmployeeShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.FirstName, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LastName, null as string);
        }

        [Fact]
        public void EmployeeShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.FirstName, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LastName, GConst.InvalidName);
        }

        [Fact]
        public void EmployeeShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.FirstName, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LastName, string.Empty);
        }
    }
}
