using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Donovan.Game;
using Donovan.Server.Storage;
using Donovan.Server.Storage.Entities;
using Donovan.Utilities;

namespace Donovan.Server.Repositories
{
    public class ManagerRepository : RepositoryBase, IManagerRepository
    {
        public ManagerRepository(IOptions<StorageOptions> options) : base(options)
        {
        }

        public async Task<Manager> CreateManagerAsync(Manager manager)
        {
            var client = this.StorageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("Manager");

            // TODO: Provision tables during startup to avoid the runtime overhead of this call.
            // TODO: Review service lifetime in dependency injection before settling on approach.
            var isCreated = await table.CreateIfNotExistsAsync()
                .ConfigureAwait(false);

            var entity = new ManagerEntity(manager);
            var operation = TableOperation.Insert(entity);

            var result = await table.ExecuteAsync(operation)
                .ConfigureAwait(false);

            var entityCreated = result.Result as ManagerEntity;
            var managerCreated = entityCreated.ToManager();

            return managerCreated;
        }

        public async Task<Manager> GetManagerAsync(string email)
        {
            var client = this.StorageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("Manager");

            // TODO: Provision tables during startup to avoid the runtime overhead of this call.
            // TODO: Review service lifetime in dependency injection before settling on approach.
            var isCreated = await table.CreateIfNotExistsAsync()
                .ConfigureAwait(false);

            var partitionKey = Base64Helper.ToBase64(email);
            var rowKey = Base64Helper.ToBase64(email);
            var operation = TableOperation.Retrieve<ManagerEntity>(partitionKey, rowKey);

            var result = await table.ExecuteAsync(operation)
                .ConfigureAwait(false);

            var entity = result.Result as ManagerEntity;
            var manager = entity.ToManager();

            return manager;
        }
    }
}
