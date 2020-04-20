using Xunit;

namespace Donovan.Tests.Core
{
    [CollectionDefinition("TestEnvironment")]
    public class TestEnvironmentCollection : ICollectionFixture<TableStorageFixture>, ICollectionFixture<WebHostFixture>
    {
    }
}
