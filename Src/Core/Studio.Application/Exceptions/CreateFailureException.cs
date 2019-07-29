namespace Studio.Application.Exceptions
{
    using Studio.Common;
    using System;

    public class CreateFailureException : Exception
    {
        private const string Create = "създаването";

        public CreateFailureException(string name, object key, string message)
            : base(string.Format(GConst.FailureException, Create, name, key, message))
        {
        }
    }
}
