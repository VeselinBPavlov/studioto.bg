namespace Studio.Application.Exceptions
{
    using System;
    using Common;
    
    public class UpdateFailureException : Exception
    {
        public const string Update = "промяната";

        public UpdateFailureException(string name, object key, string message)
            : base(string.Format(GConst.FailureException, Update, name, key, message))
        {
        }        
    }
}