namespace Studio.Domain.Exceptions
{
    using System;
    using Studio.Common;

    public class AddressFormatInvalidException : Exception
    {
        public AddressFormatInvalidException(Exception ex)
            : base(string.Format(GConst.ValueObjectExceptionMessage, GConst.AddressLower), ex)
        {
        }
    }
}