using System.Text.Json;

namespace Donovan.Utilities
{
    public static class JsonHelper
    {
        public static string ToJson<T>(T value)
        {
            return JsonSerializer.Serialize<T>(value);
        }

        public static T FromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
