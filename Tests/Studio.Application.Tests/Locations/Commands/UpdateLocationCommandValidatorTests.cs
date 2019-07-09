namespace Studio.Application.Tests.Locations.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Locations.Commands.Update;
    using Studio.Common;
    using Xunit;

    public class UpdateLocationCommandValidatorTests
    {
        private UpdateLocationCommandValidator updateValidator;
        private UpdateLocationCommand updateCommand;

        public UpdateLocationCommandValidatorTests()
        {
            this.updateValidator = new UpdateLocationCommandValidator();
            this.updateCommand = new UpdateLocationCommand();
        }

        [Fact]
        public void LocationShouldNotReturnError()
        {
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.Name, GConst.ValidName);            
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.StartDay, GConst.ValidStartDay);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.EndDay, GConst.ValidEndDay);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.StartHour, GConst.ValidStartHour);
            updateValidator.ShouldNotHaveValidationErrorFor(updateCommand => updateCommand.EndHour, GConst.ValidEndHour);
        }

        [Fact]
        public void LocationShouldReturnErrorIfNameIsNull()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.StartDay, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EndDay, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.StartHour, null as string);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EndHour, null as string);
        }

        [Fact]
        public void LocationShouldReturnErrorIfNameLongerThan100Characters()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, GConst.InvalidName);            
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.StartDay, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EndDay, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.StartHour, GConst.InvalidName);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EndHour, GConst.InvalidName);
        }

        [Fact]
        public void LocationShouldReturnErrorIfNameIsEmptyString()
        {
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.Name, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.StartDay, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EndDay, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.StartHour, string.Empty);
            updateValidator.ShouldHaveValidationErrorFor(updateCommand => updateCommand.EndHour, string.Empty);
        }
    }
}
