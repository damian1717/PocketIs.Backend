using Microsoft.Extensions.Configuration;

namespace PocketIS.Infrastucture.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IReadOnlyDictionary<string, string> SupportedCultures(this IConfiguration configuration)
        {
            return configuration.GetSection("SupportedCultures")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
