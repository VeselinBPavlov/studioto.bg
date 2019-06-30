namespace Studio.Domain.Exceptions
{
    using System;

    public class AddressFormatInvalidException : Exception
    {
        private const string Address = "Address";

        public AddressFormatInvalidException(Exception ex)
            : base(string.Format("{0} is invalid.", Address), ex)
        {
        }
    }
}