namespace Studio.Application.Tests.Locations.Commands
{
    using FluentValidation.TestHelper;
    using Studio.Application.Locations.Commands.Create;
    using Studio.Common;
    using Xunit;

    public class CreateLocationCommandValidatorTests
    {
        private CreateLocationCommandValidator createValidator;
        private CreateLocationCommand createCommand;

        public CreateLocationCommandValidatorTests()
        {
            this.createValidator = new CreateLocationCommandValidator();
            this.createCommand = new CreateLocationCommand();
        }

        [Fact]
        public void LocationShouldNotReturnError()
        {
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.Name, GConst.ValidName);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.StartDay, GConst.ValidStartDay);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.EndDay, GConst.ValidEndDay);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.StartHour, GConst.ValidStartHour);
            createValidator.ShouldNotHaveValidationErrorFor(createCommand => createCommand.EndHour, GConst.ValidEndHour);
        }

        [Fact]
        public void LocationShouldReturnErrorIfNameIsNull()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.StartDay, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EndDay, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.StartHour, null as string);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EndHour, null as string);

        }

        [Fact]
        public void LocationShouldReturnErrorIfNameLongerThan100Characters()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, GConst.InvalidName);            
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.StartDay, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EndDay, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.StartHour, GConst.InvalidName);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EndHour, GConst.InvalidName);
        }

        [Fact]
        public void LocationShouldReturnErrorIfNameIsEmptyString()
        {
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.Name, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.StartDay, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EndDay, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.StartHour, string.Empty);
            createValidator.ShouldHaveValidationErrorFor(createCommand => createCommand.EndHour, string.Empty);
        }
    }
}
