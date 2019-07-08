namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Persistence.Context;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Domain.ValueObjects;

    public class CommandTestBase : IDisposable
    {
        protected readonly StudioDbContext context;

        public CommandTestBase()
        {
            this.context = StudioDBContextFactory.Create();
        }

        public void Dispose()
        {
            StudioDBContextFactory.Destroy(this.context);
        }

 
    }
}