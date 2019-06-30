namespace Studio.Application.Exceptions
{
    using Studio.Common;
    using System;
    
    public class UpdateFailureException : Exception
    {
        public const string Update = "Update";

        public UpdateFailureException(string name, object key, string message)
            : base(string.Format(GlobalConstants.FailureException, Update, name, key, message))
        {
        }        
    }
}