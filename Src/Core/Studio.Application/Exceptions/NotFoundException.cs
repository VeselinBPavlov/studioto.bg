namespace Studio.Application.Exceptions
{
    using Studio.Common;
    using System;

    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base(string.Format(GlobalConstants.NotFoundException, name, key))
        {
        }
    }
}
