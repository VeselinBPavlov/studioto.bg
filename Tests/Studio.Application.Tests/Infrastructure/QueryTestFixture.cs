namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;
    using Studio.Persistence.Context;
    using Xunit;

    public class QueryTestFixture : IDisposable
    {
        public StudioDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        
        public QueryTestFixture()
        {
            Context = StudioDBContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }
        public void Dispose()
        {
            StudioDBContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}