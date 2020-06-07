using System;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace Donovan.Game.Storage
{
    public class RepositoryBase
    {
        protected CloudStorageAccount StorageAccount { get; }

        public RepositoryBase(IOptions<StorageOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            this.StorageAccount = CloudStorageAccount.Parse(options.Value.TableStorage);
        }
    }
}
