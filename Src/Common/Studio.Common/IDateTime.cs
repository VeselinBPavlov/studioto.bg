namespace Studio.Common
{
    using System;

    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
