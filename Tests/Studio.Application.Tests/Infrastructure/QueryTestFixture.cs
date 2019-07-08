namespace Studio.Application.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    public class QueryTestFixture : IDisposable
    {
        protected StudioDbContext context;
        protected IMapper mapper;
        
        public QueryTestFixture()
        {
            context = StudioDBContextFactory.Create();
            mapper = AutoMapperFactory.Create();
        }
        public void Dispose()
        {
            StudioDBContextFactory.Destroy(this.context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}