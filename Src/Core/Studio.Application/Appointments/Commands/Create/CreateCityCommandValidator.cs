namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using FluentValidation;

    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {            
            RuleFor(c => c.ReservationDate).NotEmpty().WithMessage("Reservation date is required").Must(this.BeValidDate).WithMessage("Invalid date");
            RuleFor(c => c.TimeBlockHelper).NotEmpty().WithMessage("Reservation hour is required");
        }

        private bool BeValidDate(DateTime reservationTime)
        {
            return reservationTime.Day >= DateTime.UtcNow.Day;
        }
    }
}
