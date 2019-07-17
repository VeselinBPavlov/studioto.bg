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
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.ReservationDate, DateTime.UtcNow.AddDays(1));
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.TimeBlockHelper, GConst.ValidName);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.TimeBlockHelper, null as string);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.TimeBlockHelper, string.Empty);
        }

                [Fact]
        public void AppointmentShouldReturnErrorIfReservationDateIsPassed()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.ReservationDate, DateTime.UtcNow.AddDays(-1));
        }
    }
}
