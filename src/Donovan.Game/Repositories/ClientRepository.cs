using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Donovan.Game.Security;
using Donovan.Game.Storage;
using Donovan.Game.Storage.Entities;

namespace Donovan.Game.Repositories
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        public ClientRepository(IOptions<StorageOptions> options) : base(options)
        {
        }

        public async Task<Client> GetClientAsync(string id)
        {
            var storageClient = this.StorageAccount.CreateCloudTableClient();
            var table = storageClient.GetTableReference("Client");

            // TODO: Provision tables during startup to avoid the runtime overhead of this call.
            // TODO: Review service lifetime in dependency injection before settling on approach.
            var isCreated = await table.CreateIfNotExistsAsync()
                .ConfigureAwait(false);

            var partitionKey = id;
            var rowKey = id;
            var operation = TableOperation.Retrieve<ClientEntity>(partitionKey, rowKey);

            var result = await table.ExecuteAsync(operation)
                .ConfigureAwait(false);

            var client = null as Client;

            if (result.HttpStatusCode == (int)HttpStatusCode.OK)
            {
                var entity = result.Result as ClientEntity;

                client = entity.ToClient();
            }

            return client;
        }
    }
}
