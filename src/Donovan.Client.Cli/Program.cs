using System;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Donovan.Client.Cli.Commands;

namespace Donovan.Client.Cli
{
    [Command(Name = "donovan", Description = "Interact with the Donovan soccer simulation.")]
    [Subcommand(typeof(RegisterCommand))]
    [VersionOptionFromMember(Description = "Displays the application version", MemberName = nameof(GetVersion))]
    public class Program
    {
        private static string email;
        private static string password;

        public static async Task<int> Main(string[] args)
            => await CommandLineApplication.ExecuteAsync<Program>(args);

        private static string GetVersion()
            => "Donovan " + typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        private async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            app.ShowHelp();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }

            return await Task.Run(() => { return 0; });
        }
    }
}
