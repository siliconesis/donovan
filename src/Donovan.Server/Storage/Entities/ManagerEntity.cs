using System;
using Microsoft;
using Microsoft.Azure;
using Microsoft.Azure.Cosmos.Table;
using Donovan;
using Donovan.Models;
using Donovan.Utilities;

namespace Donovan.Server.Storage.Entities
{
    public class ManagerEntity : TableEntity
    {
        public ManagerEntity()
        {
        }

        public ManagerEntity(Manager model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.PartitionKey = Encoder.ToBase64(model.Email);
            this.RowKey = Encoder.ToBase64(model.Email);

            this.Email = model.Email;
            this.Name = model.Name;
            this.Provider = model.Provider;
        }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Provider { get; set; }
    }
}
