using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hedin.ChangeTires.Modules.Booking.Infrastructure.Bookings.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDBContext _context;

        public BookingRepository(BookingDBContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Booking>> ListAllAsync(string customerEmail)
        {
            return await _context
                .Bookings
                .Where(b => b.CustomerEmail == customerEmail)
                .ToListAsync();
        }

        public async Task AddAsync(Domain.Entities.Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.Booking> GetBySlotIdAsync(Guid slotId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.SlotId == slotId);
        }

        public Task<bool> IsReturningCustomer(string customerEmail)
        {
            return _context.Bookings.AnyAsync(b => b.CustomerEmail == customerEmail);
        }
    }
}
