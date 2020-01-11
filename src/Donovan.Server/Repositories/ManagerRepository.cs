using System;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Models;
using Donovan.Server;
using Donovan.Server.Storage;
using Donovan.Server.Storage.Entities;
using Donovan.Utilities;
using Microsoft;
using Microsoft.Azure;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;

namespace Donovan.Server.Repositories
{
    public class ManagerRepository : RepositoryBase, IManagerRepository
    {
        public ManagerRepository(IOptions<StorageOptions> options) : base(options)
        {
        }

        public async Task<Manager> GetAsync(string id)
        {
            var client = this.StorageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("Manager");
            var isCreated = await table.CreateIfNotExistsAsync();

            var partitionKey = id;
            var rowKey = id;
            var operation = TableOperation.Retrieve<ManagerEntity>(partitionKey, rowKey);

            var result = await table.ExecuteAsync(operation);
            var entity = result.Result as ManagerEntity;

            var manager = new Manager() { Email = entity.Email, Name = entity.Name, Provider = entity.Provider };

            return manager;
        }
    }
}
