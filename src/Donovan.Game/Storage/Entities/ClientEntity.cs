using System;
using Microsoft.Azure.Cosmos.Table;
using Donovan.Game.Security;

namespace Donovan.Game.Storage.Entities
{
    public class ClientEntity : TableEntity
    {
        public ClientEntity()
        {
        }

        public ClientEntity(Client model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.PartitionKey = model.Id;
            this.RowKey = model.Id;

            this.Name = model.Name;
            this.Secret = model.Secret;
            this.IsEnabled = model.IsEnabled;
        }

        public string Name { get; set; }

        public string Secret { get; set; }

        public bool IsEnabled { get; set; }

        public Client ToClient()
        {
            var client = new Client()
            {
                Id = this.RowKey,
                Name = this.Name,
                Secret = this.Secret,
                IsEnabled = this.IsEnabled
            };

            return client;
        }
    }
}
