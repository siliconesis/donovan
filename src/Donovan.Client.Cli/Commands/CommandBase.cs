using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Donovan.Client.Cli.Commands
{
    internal class CommandBase
    {
        protected Program Parent { get; set; }

        protected virtual async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            return await Task.Run(() => { return 0; });
        }
    }
}
