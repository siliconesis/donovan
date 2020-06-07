using Xunit;

namespace Donovan.Game.Tests.Core
{
    [CollectionDefinition("TestEnvironment")]
    public class TestEnvironmentCollection : ICollectionFixture<TableStorageFixture>, ICollectionFixture<WebHostFixture>
    {
    }
}
