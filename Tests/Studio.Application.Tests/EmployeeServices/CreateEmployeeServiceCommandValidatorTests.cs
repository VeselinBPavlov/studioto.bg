namespace Studio.Application.Tests.EmployeeServices.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.EmployeeServices.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateEmployeeServiceCommandValidatorTests
    {
        private CreateEmployeeServiceCommandValidator createValidator;
        private CreateEmployeeServiceCommand createCommand;

        public CreateEmployeeServiceCommandValidatorTests()
        {
            this.createValidator = new CreateEmployeeServiceCommandValidator();
            this.createCommand = new CreateEmployeeServiceCommand();
        }

        [Fact]
        public void EmployeeServiceShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Price, GConst.ValidPrice);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.EmployeeId, GConst.ValidId);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.ServiceId, GConst.ValidId);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.DurationInMinutes, GConst.ValidServiceDuration);
        }

        [Fact]
        public void EmployeeServiceShouldReturnErrorIfEntityIsZeroOrLess()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Price, GConst.ZeroPrice);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Price, GConst.InvalidPrice);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EmployeeId, GConst.ZeroId);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.ServiceId, GConst.ZeroId);
        }

        [Fact]
        public void EmployeeServiceShouldReturnErrorForInvalidDuration()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.DurationInMinutes, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.DurationInMinutes, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.DurationInMinutes, GConst.ValidStartHour);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.DurationInMinutes, GConst.ValidStartHour + GConst.ValidStartHour + GConst.ValidStartHour);
        }
    }
}
