﻿namespace Studio.Application.Tests.EmployeeServices.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.EmployeeServices.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateEmployeeServiceCommandValidatorTests
    {
        private UpdateEmployeeServiceCommandValidator updateValidator;
        private UpdateEmployeeServiceCommand updateCommand;

        public UpdateEmployeeServiceCommandValidatorTests()
        {
            this.updateValidator = new UpdateEmployeeServiceCommandValidator();
            this.updateCommand = new UpdateEmployeeServiceCommand();
        }

        [Fact]
        public void EmployeeServiceShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Price, GConst.ValidPrice);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.EmployeeId, GConst.ValidId);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.ServiceId, GConst.ValidId);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.DurationInMinutes, GConst.ValidServiceDuration);
        }

        [Fact]
        public void EmployeeServiceShouldReturnErrorIfEntityIsZeroOrLess()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Price, GConst.ZeroPrice);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Price, GConst.InvalidPrice);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EmployeeId, GConst.ZeroId);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.ServiceId, GConst.ZeroId);
        }

        [Fact]
        public void EmployeeServiceShouldReturnErrorForInvalidDuration()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.DurationInMinutes, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.DurationInMinutes, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.DurationInMinutes, GConst.ValidStartHour);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.DurationInMinutes, GConst.ValidStartHour + GConst.ValidStartHour + GConst.ValidStartHour);
        }
    }
}
