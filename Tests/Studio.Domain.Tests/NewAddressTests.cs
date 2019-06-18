namespace Studio.Domain.Tests
{
    using Xunit;

    using Entities;
    
    public class NewAddressTests
    {
        [Fact]
        public void NewAddressTestValid()
        {
            var address = new Address
            {
                Id = 1,
                Apartment = "22",
                Floor = "2",
                Number = "3�",
                Street = "����� ������",
                District = null,
                PostalCode = null,
                Latitude = 40.545M,
                Longitude = 40.214M,
                CityId = 1
            };

            Assert.NotNull(address);
        }
    }
}
