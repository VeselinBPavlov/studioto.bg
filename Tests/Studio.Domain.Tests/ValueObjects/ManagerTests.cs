namespace Studio.Domain.Tests.ValueObjects
{
    using Studio.Domain.Exceptions;
    using Studio.Domain.ValueObjects;
    using Xunit;

    public class ManagerTests
    {
        [Fact]
        public void ShouldHaveCorrectFirstAndLastName()
        {
            var manager = Manager.For("Ivan Ivanov");

            Assert.Equal("Ivan", manager.FirstName);
            Assert.Equal("Ivanov", manager.LastName);
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            const string value = "Ivan Ivanov";

            var manager = Manager.For(value);

            Assert.Equal(value, manager.ToString());
        }

        [Fact]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string value = "Ivan Ivanov";

            var manager = Manager.For(value);

            string result = manager;

            Assert.Equal(value, result);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsFirstAndLastName()
        {
            var manager = (Manager) "Ivan Ivanov";

            Assert.Equal("Ivan", manager.FirstName);
            Assert.Equal("Ivanov", manager.LastName);
        }

        [Fact]
        public void ShouldThrowManagerInvalidExceptionForInvalidAdAccount()
        {
            Assert.Throws<ManagerInvalidException>(() => (Manager) "IvanIvanov");
        }
    }
}