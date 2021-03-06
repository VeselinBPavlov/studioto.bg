﻿namespace Studio.Domain.Exceptions
{
    using System;

    public class ManagerInvalidException : Exception
    {
        private const string Manager = "Manager";

        public ManagerInvalidException(Exception ex)
            : base(string.Format(Studio.Common.GConst.ValueObjectException, Manager), ex)
        {
        }
    }
}
