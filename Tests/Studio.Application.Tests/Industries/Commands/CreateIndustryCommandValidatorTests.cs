namespace Studio.Application.Tests.Industries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Industries.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateIndustryCommandValidatorTests
    {
        private CreateIndustryCommandValidator createValidator;
        private CreateIndustryCommand createCommand;

        public CreateIndustryCommandValidatorTests()
        {
            this.createValidator = new CreateIndustryCommandValidator();
            this.createCommand = new CreateIndustryCommand();
        }

        [Fact]
        public void IndustryShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Name, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Possition, GConst.ValidName);
        }

        [Fact]
        public void IndustryShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Possition, null as string);
        }

        [Fact]
        public void IndustryShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Possition, GConst.InvalidName);
        }

        [Fact]
        public void IndustryShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Possition, string.Empty);
        }
    }
}
