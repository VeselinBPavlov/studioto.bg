namespace Studio.Application.Appointments.Commands.Update
{
    using System;
    using Common;
    using FluentValidation;

    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        private const string ReservationDate = "Дата за резервация";
        private const string TimeBlockHelper = "Час за резервация";

        public UpdateAppointmentCommandValidator()
        {            
            RuleFor(c => c.ReservationDate)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, ReservationDate))
                .Must(this.BeValidDate)
                .WithMessage(string.Format(GConst.ErrorInvalidMessage, ReservationDate));
            RuleFor(c => c.TimeBlockHelper)
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, TimeBlockHelper));
        }

        private bool BeValidDate(DateTime reservationTime)
        {
            return reservationTime.Day >= DateTime.UtcNow.Day;
        }
    }
}
