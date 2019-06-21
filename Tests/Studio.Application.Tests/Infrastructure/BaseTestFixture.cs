using Studio.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studio.Application.Tests.Infrastructure
{
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
