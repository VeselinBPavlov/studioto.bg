namespace Studio.Domain.Tests.ValueObjects
{
    using Studio.Domain.Exceptions;
    using Studio.Domain.ValueObjects;
    using Xunit;

    public class ManagerTests
    {
        private const string FirstName = "Ivan";
        private const string LastName = "Ivanov";
        private const string InvalidName = "IvanIvanov";

        [Fact]
        public void ShouldHaveCorrectFirstAndLastName()
        {
            var manager = Manager.For($"{FirstName} {LastName}");

            Assert.Equal(FirstName, manager.FirstName);
            Assert.Equal(LastName, manager.LastName);
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            string value = $"{FirstName} {LastName}";

            var manager = Manager.For(value);

            Assert.Equal(value, manager.ToString());
        }

        [Fact]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            string value = $"{FirstName} {LastName}";

            var manager = Manager.For(value);

            string result = manager;

            Assert.Equal(value, result);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsFirstAndLastName()
        {
            var manager = (Manager) $"{FirstName} {LastName}";

            Assert.Equal(FirstName, manager.FirstName);
            Assert.Equal(LastName, manager.LastName);
        }

        [Fact]
        public void ShouldThrowManagerInvalidExceptionForInvalidAdAccount()
        {
            Assert.Throws<ManagerInvalidException>(() => (Manager) InvalidName);
        }
    }
}