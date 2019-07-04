﻿namespace Studio.Application.EmployeeServices.Commands.Update
{
    using FluentValidation;

    public class UpdateEmployeeServiceCommandValidator : AbstractValidator<UpdateEmployeeServiceCommand>
    {
        public UpdateEmployeeServiceCommandValidator()
        {
            RuleFor(es => es.Price).GreaterThan(0.00M);
            RuleFor(es => es.EmployeeId).NotEqual(0);
            RuleFor(es => es.ServiceId).NotEqual(0);
        }
    }
}
