namespace Studio.Application.Tests.Industries.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Industries.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateIndustryCommandValidatorTests
    {
        private UpdateIndustryCommandValidator updateValidator;
        private UpdateIndustryCommand updateCommand;

        public UpdateIndustryCommandValidatorTests()
        {
            this.updateValidator = new UpdateIndustryCommandValidator();
            this.updateCommand = new UpdateIndustryCommand();
        }

        [Fact]
        public void IndustryShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Name, GConst.IndustryValidName);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Possition, GConst.IndustryPossition);
        }

        [Fact]
        public void IndustryShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Possition, null as string);
        }

        [Fact]
        public void IndustryShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Possition, GConst.InvalidName);
        }

        [Fact]
        public void IndustryShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Possition, string.Empty);
        }
    }
}
