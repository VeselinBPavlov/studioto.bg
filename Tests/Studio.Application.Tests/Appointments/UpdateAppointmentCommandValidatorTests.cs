namespace Studio.Application.Tests.Appointments.Commands
{
    using System;
    using FluentValidation.TestHelper;
    using Studio.Application.Appointments.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateAppointmentCommandValidatorTests
    {
        private UpdateAppointmentCommandValidator updateValidator;
        private UpdateAppointmentCommand updateCommand;

        public UpdateAppointmentCommandValidatorTests()
        {
            this.updateValidator = new UpdateAppointmentCommandValidator();
            this.updateCommand = new UpdateAppointmentCommand();
        }

        [Fact]
        public void AppointmentShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.FirstName, GConst.ValidName);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.LastName, GConst.ValidName);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Email, GConst.ValidEmail);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Phone, GConst.ValidPhone);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.ReservetionTime, DateTime.UtcNow.AddDays(1));
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.FirstName, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LastName, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Email, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Phone, null as string);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.FirstName, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LastName, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Email, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Phone, GConst.InvalidName);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.FirstName, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LastName, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Email, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Phone, string.Empty);
        }

                [Fact]
        public void AppointmentShouldReturnErrorIfReservationDateIsPassed()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.ReservetionTime, DateTime.UtcNow.AddDays(-1));
        }
    }
}
