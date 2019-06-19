namespace Studio.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Exceptions;
    using Infrastructure;

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

            try
            {
                int index = -1;
                int next = 0;
                int counter = 0;

                while (true) 
                {
                    index = addressString.IndexOf("\\", index + 1);

                    if (index == -1)
                    {
                        break;
                    }

                    addressComponents[counter] = addressString.Substring(next, index);
                    next = index;
                    counter++;
                }

                address.Street = addressComponents[0];
                address.Number = addressComponents[1];
                address.Building = addressComponents[2];
                address.Entrance = addressComponents[3];
                address.Floor = addressComponents[4];
                address.Apartment = addressComponents[5];
                address.District = addressComponents[6];
            }
            catch (Exception ex)
            {
                throw new AdAccountInvalidException(addressString, ex);
            }

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

            sb.Append($"ул. {this.Street}\\ №{this.Number}\\");

            if (this.Building != null)
            {
                sb.Append($"бл. {this.Building}\\");
            }

            if (this.Entrance != null)
            {
                sb.Append($"вх. {this.Entrance}\\");
            }

            if (this.Floor != null)
            {
                sb.Append($"ет. {this.Floor}\\");
            }

            if (this.Apartment != null)
            {
                sb.Append($"ап. {this.Apartment}\\");
            }

            if (this.District != null)
            {
                sb.Append($"кв. {this.District}\\");
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