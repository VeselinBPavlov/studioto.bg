namespace Studio.Domain.Exceptions
{
    using System;

    public class AddressFormatInvalidException : Exception
    {
        public AddressFormatInvalidException(string address, Exception ex)
            : base($"Address \"{address}\" is invalid.", ex)
        {
        }
    }
}