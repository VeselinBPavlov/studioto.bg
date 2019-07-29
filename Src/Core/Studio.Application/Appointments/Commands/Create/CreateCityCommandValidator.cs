namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using FluentValidation;
    using Studio.Common;

    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        private const string ReservationDate = "Дата за резервация";
        private const string TimeBlockHelper = "Час за резервация";

        public CreateAppointmentCommandValidator()
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
