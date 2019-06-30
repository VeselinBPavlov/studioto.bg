namespace Studio.Application.Exceptions
{
    using Studio.Common;
    using System;

    public class DeleteFailureException : Exception
    {
        private const string Delete = "Deletion";

        public DeleteFailureException(string name, object key, string message)
            : base(string.Format(GlobalConstants.FailureException, Delete, name, key, message))
        {
        }
    }
}
