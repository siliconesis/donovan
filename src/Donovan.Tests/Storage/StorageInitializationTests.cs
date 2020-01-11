using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Donovan;
using Donovan.Models;
using Donovan.Server;
using Donovan.Server.Storage;
using Donovan.Server.Storage.Entities;
using Microsoft;
using Microsoft.Azure;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Donovan.Tests.Storage
{
    public class StorageInitializationTests
    {
        private IConfiguration Configuration { get; }

        private string ConnectionString { get; }

        public StorageInitializationTests()
        {
            this.Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            this.ConnectionString = this.Configuration["ConnectionStrings:DataStorage"];
        }

        [Fact]
        public async Task Initialize()
        {
            var entities = new List<ManagerEntity>()
            {
                new ManagerEntity(new Manager() { Email = "fred@bedrock.com", Name = "Fred Flintstone", Provider = "Microsoft" }),
                new ManagerEntity(new Manager() { Email = "wilma@bedrock.com", Name = "Wilma Flintstone", Provider = "Microsoft" })
            };

            // Connect to storage.
            var account = CloudStorageAccount.Parse(this.ConnectionString);
            var client = account.CreateCloudTableClient();

            // Create table.
            var table = client.GetTableReference("Manager");
            var isDeleted = await table.DeleteIfExistsAsync();
            var isCreated = await table.CreateIfNotExistsAsync();

            Assert.True(isCreated);

            // Insert rows (cannot batch insert due to varying partition keys).
            foreach (var entity in entities)
            {
                Console.WriteLine($"Inserting {entity.Name}...");

                var operation = TableOperation.Insert(entity);
                var result = await table.ExecuteAsync(operation);

                Console.WriteLine($"Status: {result.HttpStatusCode}");

                Assert.Equal(204, result.HttpStatusCode);
            }
        }
    }
}
