using System;
using Microsoft;
using Microsoft.Azure;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;

namespace Donovan.Server.Storage
{
    public class RepositoryBase
    {
        protected CloudStorageAccount StorageAccount { get; }

        public RepositoryBase(IOptions<StorageOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            this.StorageAccount = CloudStorageAccount.Parse(options.Value.DataStorage);
        }
    }
}
