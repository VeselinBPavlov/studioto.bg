namespace Studio.Domain.Exceptions
{
    using System;

public class AdAccountInvalidException : Exception
    {
        public AdAccountInvalidException(string address, Exception ex)
            : base($"Address \"{address}\" is invalid.", ex)
        {
        }
    }
}