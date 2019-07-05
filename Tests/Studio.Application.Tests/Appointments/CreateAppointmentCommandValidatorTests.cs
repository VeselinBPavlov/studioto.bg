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
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.FirstName, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.LastName, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Email, GConst.ValidEmail);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Phone, GConst.ValidPhone);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.ReservetionTime, DateTime.UtcNow.AddDays(1));
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.ReservetionDate, DateTime.UtcNow.AddDays(1));
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.TimeBlockHelper, GConst.ValidName);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Phone, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.TimeBlockHelper, null as string);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Phone, GConst.InvalidName);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.FirstName, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LastName, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Email, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Phone, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.TimeBlockHelper, string.Empty);
        }

        [Fact]
        public void AppointmentShouldReturnErrorIfReservationDateIsPassed()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.ReservetionTime, DateTime.UtcNow.AddDays(-1));
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.ReservetionDate, DateTime.UtcNow.AddDays(-1));
        }
    }
}
