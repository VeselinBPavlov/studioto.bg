namespace Studio.Application.Tests.LocationIndustries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.LocationIndustries.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateLocationIndustryCommandValidatorTests
    {
        private UpdateLocationIndustryCommandValidator updateValidator;
        private UpdateLocationIndustryCommand updateCommand;

        public UpdateLocationIndustryCommandValidatorTests()
        {
            this.updateValidator = new UpdateLocationIndustryCommandValidator();
            this.updateCommand = new UpdateLocationIndustryCommand();
        }

        [Fact]
        public void LocationIndustryShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Description, GConst.ValidName);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.LocationId, GConst.ValidId);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.IndustryId, GConst.ValidId);
        }

        [Fact]
        public void LocationIndustryShouldReturnErrorIfEntityIsZeroOrLess()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Description, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Description, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.LocationId, GConst.ZeroId);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.IndustryId, GConst.ZeroId);
        }
    }
}
