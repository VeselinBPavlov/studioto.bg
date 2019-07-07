namespace Studio.Application.Exceptions
{
    using Studio.Common;
    using System;

    public class CreateFailureException : Exception
    {
        public CreateFailureException(string name, object key, string message)
            : base(string.Format(GConst.FailureException, GConst.Create, name, key, message))
        {
        }
    }
}
