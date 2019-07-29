namespace Studio.Application.Employees.Commands.Update
{
    using FluentValidation;
    using Studio.Common;

    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private const string FirstName = "Име";
        private const string LastName = "Фамилия";

        public UpdateEmployeeCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, FirstName, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, FirstName));
            RuleFor(c => c.LastName)
                .MaximumLength(100)
                .WithMessage(string.Format(GConst.ErrorLengthMessage, LastName, 1, 100))
                .NotEmpty()
                .WithMessage(string.Format(GConst.ErrorRequiredMessage, LastName));
        }
    }
}
