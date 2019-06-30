namespace Studio.Application.Exceptions
{
    using Studio.Common;
    using System;

    public class CreateFailureException : Exception
    {
        private const string Create = "Creation";

        public CreateFailureException(string name, object key, string message)
            : base(string.Format(Common.GConst.FailureException, Create, name, key, message))
        {
        }
    }
}
