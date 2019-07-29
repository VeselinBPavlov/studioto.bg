namespace Studio.Application.Employees.Commands.Create
{
    using FluentValidation;
    using Studio.Common;

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private const string FirstName = "Име";
        private const string LastName = "Фамилия";

        public CreateEmployeeCommandValidator()
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
