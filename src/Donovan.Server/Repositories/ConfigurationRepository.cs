using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Donovan.Configuration;
using Donovan.Server.Storage;
using Donovan.Server.Storage.Entities;

namespace Donovan.Server.Repositories
{
    public class ConfigurationRepository : RepositoryBase, IConfigurationRepository
    {
        public ConfigurationRepository(IOptions<StorageOptions> options) : base(options)
        {
        }

        public async Task<Setting> GetSettingAsync(string ns, string key)
        {
            var client = this.StorageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("Configuration");

            // TODO: Provision tables during startup to avoid the runtime overhead of this call.
            // TODO: Review service lifetime in dependency injection before settling on approach.
            var isCreated = await table.CreateIfNotExistsAsync()
                .ConfigureAwait(false);

            var partitionKey = ns;
            var rowKey = key;
            var operation = TableOperation.Retrieve<ConfigurationEntity>(partitionKey, rowKey);

            var result = await table.ExecuteAsync(operation)
                .ConfigureAwait(false);

            var entity = result.Result as ConfigurationEntity;

            var setting = entity.ToSetting();

            return setting;
        }
    }
}
