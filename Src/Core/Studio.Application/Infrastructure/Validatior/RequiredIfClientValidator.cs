namespace Studio.Application.Infrastructure.Validatior
{
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using FluentValidation.Internal;
    using FluentValidation.Resources;
    using FluentValidation.Validators;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public class RequiredIfClientValidator : ClientValidatorBase
    {
        public RequiredIfClientValidator(PropertyRule rule, IPropertyValidator validator) : base(rule, validator)
        {
        }

        public RequiredIfValidator RequiredIfValidator
        {
            get { return (RequiredIfValidator)Validator; }
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredif", this.GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-requiredif-dependentproperty", RequiredIfValidator.DependentProperty);
            MergeAttribute(context.Attributes, "data-val-requiredif-targetvalue", RequiredIfValidator.TargetValue.ToString());
        }

        private string GetErrorMessage(ClientModelValidationContext context)
        {
            var formatter = ValidatorOptions.MessageFormatterFactory().AppendPropertyName(Rule.GetDisplayName());
            string messageTemplate;
            try
            {
                messageTemplate = Validator.Options.ErrorMessageSource.GetString(null);
            }
            catch (FluentValidationMessageFormatException)
            {
                messageTemplate = ValidatorOptions.LanguageManager.GetStringForValidator<NotEmptyValidator>();
            }

            var message = formatter.BuildMessage(messageTemplate);
            return message;
        }
    }
}
