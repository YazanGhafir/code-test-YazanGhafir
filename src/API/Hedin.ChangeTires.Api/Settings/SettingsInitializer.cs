using System.Reflection;

namespace Hedin.ChangeTires.Api.Settings
{
    public class SettingsInitializer
    {
        public static void Initialize(
            IServiceCollection services,
            IConfiguration configuration,
            List<Type> settingTypes)
        {
            settingTypes.ForEach(serviceType =>
            {
                ConfigureSettingsInitializer(services, configuration, serviceType);
            });
        }
        
        private static void ConfigureSettingsInitializer(
            IServiceCollection services,
            IConfiguration configuration,
            Type settingsType)
        {
            var method = typeof(SettingsInitializer)
                .GetMethod(nameof(ConfigureSettingInitializerGeneric), BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(settingsType);

            method?.Invoke(null, new object[] { services, configuration });
        }

        private static void ConfigureSettingInitializerGeneric<TSettings>(
            IServiceCollection services,
            IConfiguration configuration)
            where TSettings : class
        {
            services.Configure<TSettings>(options =>
            {
                typeof(TSettings)
                    .GetProperties()
                    .ToList()
                    .ForEach(property =>
                    {
                        var value = configuration.GetValue(
                            property.PropertyType,
                            $"{typeof(TSettings).Name}:{property.Name}");

                        if (value != null)
                        {
                            property.SetValue(options, value);
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                $"Configuration Validation Error: " +
                                $"Configuration value for" +
                                $" {typeof(TSettings).Name}:{property.Name} is missing.");
                        }
                    });
            });
        }
    }
}
