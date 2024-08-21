using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Hedin.ChangeTires.Modules.Booking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Services
{
    public class BookingLister : IBookingLister
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingLister(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<BookingDto>> AllBookingsAsync(string customerEmail)
        {
            var bookings = await _bookingRepository.ListAllAsync(customerEmail);

            return bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                CustomerEmail = b.CustomerEmail,
                Date = b.Date,
                SlotId = b.SlotId,
                Price = b.Price,
            }).ToList();
        }
    }
}
