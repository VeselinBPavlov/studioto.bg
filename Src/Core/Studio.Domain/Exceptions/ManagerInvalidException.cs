namespace Studio.Domain.Exceptions
{
    using System;

    public class ManagerInvalidException : Exception
    {
        public ManagerInvalidException(string manager, Exception ex)
            : base($"Manager \"{manager}\" is invalid.", ex)
        {
        }
    }
}
