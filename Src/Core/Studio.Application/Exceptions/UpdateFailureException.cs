namespace Studio.Application.Exceptions
{
    using System;
    
    public class UpdateFailureException : Exception
    {
        public UpdateFailureException(string name, object key, string message)
            : base($"Update of entity \"{name}\" ({key}) failed. {message}")
        {
        }        
    }
}