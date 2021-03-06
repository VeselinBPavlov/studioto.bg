﻿namespace Studio.Application.Exceptions
{
    using System;
    using Common;

    public class DeleteFailureException : Exception
    {
        private const string Delete = "изтриването";

        public DeleteFailureException(string name, object key, string message)
            : base(string.Format(GConst.FailureException, Delete, name, key, message))
        {
        }
    }
}
