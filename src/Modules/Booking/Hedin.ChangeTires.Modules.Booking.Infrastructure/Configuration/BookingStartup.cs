using System.ComponentModel;
using System.Net;
using Hedin.ChangeTires.BuildingBlocks.Infrastructure.EventBus;
using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Hedin.ChangeTires.Modules.Booking.Application.Common.Services;
using Hedin.ChangeTires.Modules.Booking.Application.Pricing;
using Hedin.ChangeTires.Modules.Booking.Infrastructure.Bookings.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hedin.ChangeTires.Modules.Booking.Infrastructure.Configuration
{
    public class BookingStartup
    {
        public static void Initialize(
            string connectionString,
            IServiceCollection services,
            ILogger logger)
        {
            string moduleName = nameof(BookingStartup).Replace("Startup", string.Empty);
            ILogger moduleLogger = logger.ForContext("Module", moduleName);

            ConfigureContainer(connectionString, services, moduleLogger);
        }

        private static void ConfigureContainer(
            string connectionString,
            IServiceCollection services,
            ILogger logger)
        {
            // add db services
            services.AddDbContext<BookingDBContext>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            // load the pricing configuration
            var pricingConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "pricingConfig.json");
            var pricingConfig = PricingConfig.LoadFromJson(pricingConfigPath);
            services.AddSingleton(pricingConfig);

            // add services
            services.AddScoped<IBookingLister, BookingLister>();
            services.AddScoped<IPriceCalculator, PriceCalculator>();
            services.AddScoped<IBooker, Booker>();
            services.AddScoped<ISlotApiService, SlotApiService>();
        }
    }
}
