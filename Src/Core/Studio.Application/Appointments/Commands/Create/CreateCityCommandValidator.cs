namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using FluentValidation;

    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
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
