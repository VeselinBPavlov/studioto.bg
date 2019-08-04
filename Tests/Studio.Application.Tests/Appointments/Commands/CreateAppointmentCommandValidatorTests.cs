namespace Studio.Application.Tests.Appointments.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Appointments.Commands.Create;
    using Studio.Common;
    using System;
    using System.Globalization;
    using Xunit;

    public class CreateAppointmentCommandValidatorTests
    {
        private CreateAppointmentCommandValidator createValidator;
        private CreateAppointmentCommand createCommand;

        public CreateAppointmentCommandValidatorTests()
        {
            this.createValidator = new CreateAppointmentCommandValidator();
            this.createCommand = new CreateAppointmentCommand();
        }

        [Fact]
        public void AppointmentShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.ReservationDate, DateTime.UtcNow.AddDays(1));
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.TimeBlockHelper, GConst.ValidName);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.TimeBlockHelper, null as string);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.TimeBlockHelper, string.Empty);
        }
    }
}
