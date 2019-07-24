namespace Studio.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Exceptions;
    using Infrastructure;
    using Studio.Common;

    public class AddressFormat : ValueObject<AddressFormat>
    { 
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

        public static AddressFormat For(InputAddressData addressData)
        {   
            if (addressData.Street == null || addressData.Number == null) 
            {
                throw new AddressFormatInvalidException(new ArgumentException(Studio.Common.GConst.AddressFormatException));
            }

            var address = new AddressFormat()
            {
                Street = addressData.Street,
                Number = addressData.Number,
                Building = addressData.Building,
                Entrance = addressData.Entrance,
                Floor = addressData.Floor,
                Apartment = addressData.Apartment,
                District = addressData.District
            };          
            
            return address;
        }

        public static implicit operator string(AddressFormat address)
        {
            return address.ToString();
        }

        public static explicit operator AddressFormat(InputAddressData addressData)
        {
            return For(addressData);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"ул. {this.Street} №{this.Number}");

            if (this.Building != null)
            {
                sb.Append($", бл.{this.Building}");
            }

            if (this.Entrance != null)
            {
                sb.Append($", вх.{this.Entrance}");
            }

            if (this.Floor != null)
            {
                sb.Append($", ет.{this.Floor}");
            }

            if (this.Apartment != null)
            {
                sb.Append($", ап.{this.Apartment}");
            }

            if (this.District != null)
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