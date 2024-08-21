using Hedin.ChangeTires.BuildingBlocks.Infrastructure.Configurations;
using Hedin.ChangeTires.Modules.Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hedin.ChangeTires.Modules.Booking.Infrastructure
{
    public class BookingDBContext : DbContext
    {
        internal DbSet<Domain.Entities.Booking> Bookings { get; set; }

        private readonly CommonSettings _commonSettings;

        public BookingDBContext(IOptions<CommonSettings> settings, DbContextOptions options)
            : base(options)
        {
            _commonSettings = settings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_commonSettings.ConnectionString);
        }
    }
}