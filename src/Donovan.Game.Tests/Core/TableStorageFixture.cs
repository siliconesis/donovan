using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Donovan.Configuration;
using Donovan.Game.Security;
using Donovan.Game.Storage.Entities;

namespace Donovan.Game.Tests.Core
{
    public class TableStorageFixture
    {
        private IConfiguration Configuration { get; }

        private string ConnectionString { get; }

        public TableStorageFixture()
        {
            this.Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            this.ConnectionString = this.Configuration["Tests:StorageConnectionString"];

            //InitializeTableStorage();
        }

        private void CreateConfiguration()
        {
            // Connect to storage.
            var account = CloudStorageAccount.Parse(this.ConnectionString);
            var client = account.CreateCloudTableClient();

            // Create tables.
            var table = client.GetTableReference("Configuration");
            table.Create();

            // Create configuration settings.
            var settings = new List<Setting>()
            {
                new Setting() { Namespace = "Token", Key = "SigningKey", Value = "7yccElovdbvWnGIzzSrd2TkeejPCWr3F" }
            };

            foreach (var setting in settings)
            {
                // Create setting.
                var entity = new ConfigurationEntity(setting);
                var operation = TableOperation.Insert(entity);

                var result = table.Execute(operation);
            }
        }

        private void CreateClients()
        {
            // Connect to storage.
            var account = CloudStorageAccount.Parse(this.ConnectionString);
            var tableClient = account.CreateCloudTableClient();

            var table = tableClient.GetTableReference("Client");
            table.Create();

            // Create clients.
            var clients = new List<Donovan.Game.Security.Client>()
            {
                new Donovan.Game.Security.Client() { Id = "donovan_tests",          Name = "Donovan Tests",            Secret = PasswordHelper.HashPassword("QJIE3ZQeY1f10K146DA7IkpWy4bMXnUG"), IsEnabled = true },
                new Donovan.Game.Security.Client() { Id = "donovan_tests_disabled", Name = "Donovan Tests (Disabled)", Secret = PasswordHelper.HashPassword("ZUq6k7DG86Z6usIjMOZ0xY8hnTZGiAKg"), IsEnabled = false }
            };

            foreach (var client in clients)
            {
                // Create client.
                var entity = new ClientEntity(client);
                var operation = TableOperation.Insert(entity);

                var result = table.Execute(operation);
            }
        }

        private void CreateManagers()
        {
            // Connect to storage.
            var account = CloudStorageAccount.Parse(this.ConnectionString);
            var tableClient = account.CreateCloudTableClient();

            // Create tables.
            var table = tableClient.GetTableReference("Manager");
            table.Create();

            // Create managers.
            var managers = new List<Manager>()
            {
                new Manager()
                {
                    Id = Guid.Parse("d4d97104-217e-4220-b6a7-969fd074ce71"),
                    Name = "Fred Flintstone",
                    Email = "fred@bedrock.com",
                    Password = PasswordHelper.HashPassword("t2hMtAr37q7e487JPKyqJ2e7yNcJWVDk"),
                    RegisteredOn = DateTime.UtcNow,
                    Roles = { "Manager", "Administrator" },
                    //Teams = { },
                    VerificationStatus = VerificationStatus.Verified,
                    VerificationCode = Guid.NewGuid(),
                    VerificationCodeExpiresOn = DateTime.UtcNow.AddHours(1)
                },
                new Manager()
                {
                    Id = Guid.Parse("9b2d3334-723a-413d-96c1-06d00be9ce0a"),
                    Name = "Wilma Flintstone",
                    Email = "wilma@bedrock.com",
                    Password = PasswordHelper.HashPassword("5yjdAZ2scFKupyUKFCeE575g7DZ89mvt"),
                    RegisteredOn = DateTime.UtcNow,
                    Roles = { },
                    //Teams = { },
                    VerificationStatus = VerificationStatus.Pending,
                    VerificationCode = Guid.NewGuid(),
                    VerificationCodeExpiresOn = DateTime.UtcNow.AddHours(1)
                }
            };

            // Note that inserts cannot be batched due to varying partition keys.
            foreach (var manager in managers)
            {
                // Create manager.
                var entity = new ManagerEntity(manager);
                var operation = TableOperation.Insert(entity);

                var result = table.Execute(operation);
            }
        }

        private void DeleteTables()
        {
            // Connect to storage.
            var account = CloudStorageAccount.Parse(this.ConnectionString);
            var client = account.CreateCloudTableClient();

            // Remove tables.
            var tables = client.ListTables();

            foreach (var table in tables)
            {
                table.Delete();
            }
        }

        /// <summary>
        /// Initializes table storage before running tests.
        /// </summary>
        /// <remarks>
        /// This design intentionally places cleanup before initialization instead of after
        /// in order to enable examination of test system state after tests complete.
        /// 
        /// Environments can explicitly clean up test data if they prefer (e.g. by deleting
        /// services or resource groups in Azure).
        /// </remarks>
        private void InitializeTableStorage()
        {
            DeleteTables();

            CreateConfiguration();
            CreateClients();
            CreateManagers();
        }
    }
}
