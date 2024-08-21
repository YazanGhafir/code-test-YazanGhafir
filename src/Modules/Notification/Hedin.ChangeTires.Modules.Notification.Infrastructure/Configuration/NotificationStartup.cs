using Hedin.ChangeTires.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hedin.ChangeTires.Modules.Notification.Infrastructure
{
    public class NotificationStartup
    {
        public static void Initialize(
            string connectionString,
            IServiceCollection services,
            ILogger logger,
            IEventsBus eventsBus)
        {
            string moduleName = nameof(NotificationStartup).Replace("Startup", string.Empty);
            ILogger moduleLogger = logger.ForContext("Module", moduleName);

            ConfigureContainer(connectionString, services, moduleLogger, eventsBus);
        }

        private static void ConfigureContainer(
            string connectionString,
            IServiceCollection services,
            ILogger logger,
            IEventsBus eventsBus)
        {
            //services.AddScoped<, >();
        }
    }
}
