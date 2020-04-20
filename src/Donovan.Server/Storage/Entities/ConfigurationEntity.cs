using System;
using Microsoft.Azure.Cosmos.Table;
using Donovan.Configuration;

namespace Donovan.Server.Storage.Entities
{
    public class ConfigurationEntity : TableEntity
    {
        public ConfigurationEntity()
        {
        }

        public ConfigurationEntity(Setting model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.PartitionKey = model.Namespace;
            this.RowKey = model.Key;

            this.Value = model.Value;
        }

        public string Value { get; set; }

        public Setting ToSetting()
        {
            var setting = new Setting()
            {
                Namespace = this.PartitionKey,
                Key = this.RowKey,
                Value = this.Value
            };

            return setting;
        }
    }
}
