namespace Studio.Application.Infrastructure.Validatior
{
    using FluentValidation.Resources;
    using FluentValidation.Validators;

    public class RequiredIfValidator : PropertyValidator
    {
        public string DependentProperty { get; set; }

        public object TargetValue { get; set; }

        public RequiredIfValidator(string dependentProperty, object targetValue) : base(new LanguageStringSource(nameof(RequiredIfValidator)))
        {
            this.DependentProperty = dependentProperty;
            this.TargetValue = targetValue;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            // This is not a server side validation rule. So, should not effect at the server side
            return true;
        }
    }
}
