namespace Studio.Application.Appointments.Commands.Update
{
    using FluentValidation;
    using System;

    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(c => c.ReservationDate).NotEmpty().Must(this.BeValidDate);
            RuleFor(c => c.TimeBlockHelper).NotEmpty();
        }

        private bool BeValidDate(DateTime reservationTime)
        {
            return reservationTime.Day >= DateTime.UtcNow.Day;
        }
    }
}
