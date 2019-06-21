namespace Studio.Application.Tests.Infrastructure
{
    using AutoMapper;
    using Xunit;

    public class QueryTestFixture : BaseTestFixture
    {
        public IMapper Mapper { get; private set; }
        
        public QueryTestFixture()
        {
            Mapper = AutoMapperFactory.Create();
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}