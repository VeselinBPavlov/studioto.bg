namespace Studio.Application.Tests.LocationIndustries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.LocationIndustries.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateLocationIndustryCommandValidatorTests
    {
        private CreateLocationIndustryCommandValidator createValidator;
        private CreateLocationIndustryCommand createCommand;

        public CreateLocationIndustryCommandValidatorTests()
        {
            this.createValidator = new CreateLocationIndustryCommandValidator();
            this.createCommand = new CreateLocationIndustryCommand();
        }

        [Fact]
        public void LocationIndustrieshouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Description, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.LocationId, GConst.ValidId);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.IndustryId, GConst.ValidId);
        }

        [Fact]
        public void LocationIndustrieshouldReturnErrorIfEntityIsZeroOrLess()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Description, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Description, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.LocationId, GConst.ZeroId);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.IndustryId, GConst.ZeroId);
        }
    }
}
