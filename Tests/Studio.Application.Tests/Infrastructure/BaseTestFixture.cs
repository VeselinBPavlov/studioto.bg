namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using Studio.Persistence.Context;

    public abstract class BaseTestFixture : IDisposable
    {
        public StudioDbContext Context { get; private set; }

        public BaseTestFixture()
        {
            Context = StudioDBContextFactory.Create();
        }

        public void Dispose()
        {
            StudioDBContextFactory.Destroy(Context);
        }
    }
}
