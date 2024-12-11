using Microsoft.Extensions.Configuration;

namespace MCBackupSync.Service
{
    public static class AppSettingsService
    {

        private static IConfigurationRoot? configuration;

        public static IConfigurationRoot Configuration
        {
            get
            {
                configuration ??= LoadAppSettings();

                return configuration;
            }
        }

        public static string GetString(string key)
        {
            string value = Configuration[key] ?? string.Empty;
            return value;
        }

        public static int GetInt(string key) 
        {
            string value = Configuration[key] ?? string.Empty;
            _ = int.TryParse(value, out int result);
            return result;
        }

        private static IConfigurationRoot LoadAppSettings()
        {
            return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }

    }
}
