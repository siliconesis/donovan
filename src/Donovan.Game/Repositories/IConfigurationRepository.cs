using System.Threading.Tasks;
using Donovan.Configuration;

namespace Donovan.Game.Repositories
{
    public interface IConfigurationRepository
    {
        Task<Setting> GetSettingAsync(string ns, string key);
    }
}
