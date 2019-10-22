using System;
using Microsoft;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Donovan.Server.Storage
{
    public class RepositoryBase
    {
        protected CloudStorageAccount StorageAccount { get; }

        public RepositoryBase(IOptions<StorageOptions> options)
        {
            this.StorageAccount = CloudStorageAccount.Parse(options.Value.DataStorage);
        }
    }
}
