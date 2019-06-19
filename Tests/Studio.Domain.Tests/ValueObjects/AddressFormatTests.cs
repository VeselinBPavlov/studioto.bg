namespace Studio.Domain.Tests.ValueObjects
{
    using Studio.Domain.Exceptions;
    using Studio.Domain.ValueObjects;
    using Xunit;

    public class AddressFormatTests
    {
        private const string Separator = "<BREAK>";
        private const string Street = "Васил Левски";
        private const string Number = "18";
        private const string Building = "28";
        private const string Entrance = "3В";
        private const string Value = "ул. Васил Левски №18, бл.28, вх.3В";

        [Fact]
        public void ShouldHaveCorrectStreetNumberBuildingAndEntrance()
        {
            var address = AddressFormat.For($"{Street}{Separator}{Number}{Separator}{Building}{Separator}{Entrance}");

            Assert.Equal(Street, address.Street);
            Assert.Equal(Number, address.Number);
            Assert.Equal(Building, address.Building);
            Assert.Equal(Entrance, address.Entrance);
            Assert.Equal(string.Empty, address.Floor);
            Assert.Equal(string.Empty, address.Apartment);
            Assert.Equal(string.Empty, address.District);
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            string addressString = $"{Street}{Separator}{Number}{Separator}{Building}{Separator}{Entrance}";

            string address = AddressFormat.For(addressString);            
            string result = address.ToString();

            Assert.Equal(Value, result);
        }

        [Fact]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            string addressString = $"{Street}{Separator}{Number}{Separator}{Building}{Separator}{Entrance}";

            var address = AddressFormat.For(addressString);
            string result = address;

            Assert.Equal(Value, result);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsStreetAndNumber()
        {
            var address = (AddressFormat) $"{Street}{Separator}{Number}";

            Assert.Equal(Street, address.Street);
            Assert.Equal(Number, address.Number);
        }

        [Fact]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            Assert.Throws<AdAccountInvalidException>(() => (AddressFormat) Street);          
        }
    }
}