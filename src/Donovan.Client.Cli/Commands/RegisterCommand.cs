using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Donovan.Client.Cli.Commands
{
    [Command(Description = "Register a new manager")]
    internal class RegisterCommand : CommandBase
    {
        [Required]
        [Option(LongName = "email", ShortName = "e", ValueName = "user@domain.com", Description = "The email address used to sign in")]
        public string Email { get; }

        [Required]
        [AllowedValues("Facebook", "Google", "Microsoft", IgnoreCase = true)]
        [Option(LongName = "provider", ShortName = "p", ValueName = "Facebook|Google|Microsoft", Description = "The authentication provider used to sign in")]
        public string Provider { get; }

        protected override async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            return await Task.Run(() => { return 0; });
        }
    }
}
