namespace Studio.Application.Exceptions
{
    using System;
    using Common;

    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base(string.Format(GConst.NotFoundException, name, key))
        {
        }
    }
}
