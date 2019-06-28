namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using Persistence.Context;

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