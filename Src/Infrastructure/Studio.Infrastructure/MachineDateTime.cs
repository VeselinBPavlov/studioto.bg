namespace Studio.Infrastructure
{
    using System;

    using Common;

    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;

        public int CurrentYear => DateTime.UtcNow.Year;
    }
}
