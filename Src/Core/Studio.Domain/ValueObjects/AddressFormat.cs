namespace Studio.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Exceptions;
    using Infrastructure;

    public class AddressFormat : ValueObject<AddressFormat>
    {
        private const string Separator = "<BREAK>";
        private AddressFormat()
        {
        }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string District { get; set; }

        public static AddressFormat For(string addressString)
        {
            var addressComponents = new Dictionary<int, string>()
            {
               { 0, string.Empty },
               { 1, string.Empty },
               { 2, string.Empty },
               { 3, string.Empty },
               { 4, string.Empty },
               { 5, string.Empty },
               { 6, string.Empty }
            };

            var address = new AddressFormat();

            string[] addressData = addressString.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            int counter = 0;

            if (addressData.Length < 2) 
            {
                throw new AdAccountInvalidException(addressString, new ArgumentException("Ivalid address format!"));
            }

            foreach (var data in addressData) 
            {                   
                addressComponents[counter] = data;                   
                counter++;
            }

            address.Street = addressComponents[0];
            address.Number = addressComponents[1];
            address.Building = addressComponents[2];
            address.Entrance = addressComponents[3];
            address.Floor = addressComponents[4];
            address.Apartment = addressComponents[5];
            address.District = addressComponents[6];
            
            return address;
        }

        public static implicit operator string(AddressFormat address)
        {
            return address.ToString();
        }

        public static explicit operator AddressFormat(string addressString)
        {
            return For(addressString);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"ул. {this.Street} №{this.Number}");

            if (this.Building != string.Empty)
            {
                sb.Append($", бл.{this.Building}");
            }

            if (this.Entrance != string.Empty)
            {
                sb.Append($", вх.{this.Entrance}");
            }

            if (this.Floor != string.Empty)
            {
                sb.Append($", ет.{this.Floor}");
            }

            if (this.Apartment != string.Empty)
            {
                sb.Append($", ап.{this.Apartment}");
            }

            if (this.District != string.Empty)
            {
                sb.Append($", кв. {this.District}");
            }

            return sb.ToString().TrimEnd();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Street;
            yield return this.Number;
            yield return this.Building;
            yield return this.Entrance;
            yield return this.Floor;
            yield return this.Apartment;
            yield return this.District;
        }
    }
}