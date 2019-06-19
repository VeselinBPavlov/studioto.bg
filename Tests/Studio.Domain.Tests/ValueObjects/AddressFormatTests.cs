namespace Studio.Domain.Tests.ValueObjects
{
    using Studio.Domain.Exceptions;
    using Studio.Domain.ValueObjects;
    using Xunit;

    public class AddressFormatTests
    {
        private const string Street = "Васил Левски";
        private const string Number = "18";
        private const string Building = "28";
        private const string Entrance = "3В";
        private const string Value = "ул. Васил Левски №18, бл.28, вх.3В";

        private InputAddressData addressData = new InputAddressData 
        {
            Street = Street,
            Number = Number,
            Building = Building,
            Entrance = Entrance
        };

        [Fact]
        public void ShouldHaveCorrectStreetNumberBuildingAndEntrance()
        {
            var address = AddressFormat.For(addressData);

            Assert.Equal(Street, address.Street);
            Assert.Equal(Number, address.Number);
            Assert.Equal(Building, address.Building);
            Assert.Equal(Entrance, address.Entrance);
            Assert.Equal(null, address.Floor);
            Assert.Equal(null, address.Apartment);
            Assert.Equal(null, address.District);
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            string address = AddressFormat.For(addressData);            
            string result = address.ToString();

            Assert.Equal(Value, result);
        }

        [Fact]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            var address = AddressFormat.For(addressData);
            string result = address;

            Assert.Equal(Value, result);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsStreetAndNumber()
        {
            var address = (AddressFormat) addressData;

            Assert.Equal(Street, address.Street);
            Assert.Equal(Number, address.Number);
        }

        [Fact]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAddress()
        {
            addressData.Number = null;
            Assert.Throws<AdAccountInvalidException>(() => (AddressFormat) addressData);          
        }
    }
}