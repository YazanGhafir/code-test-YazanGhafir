using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Hedin.ChangeTires.Modules.Booking.Application.Dtos;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Services
{
    public class Booker : IBooker
    {
        private readonly ISlotApiService _slotApiService;
        private readonly IBookingRepository _bookingRepository;

        public Booker(ISlotApiService slotApiService, IBookingRepository bookingRepository)
        {
            _slotApiService = slotApiService;
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingConfirmationResponse> BookTireChangeAsync(Guid slotId, string customerEmail, decimal price)
        {
            var slot = await _slotApiService.GetSlotAsync(slotId);

            // Validate that there is no booking for the slot
            var existingBooking = await _bookingRepository.GetBySlotIdAsync(slotId);
            if (existingBooking != null)
            {
                return BookingConfirmationResponse.Failed("Slot already booked");
            }

            var bookingConfirmed = await _slotApiService.ConfirmBookingAsync(slotId);

            if (bookingConfirmed)
            {
                bool isReturningCustomer = await _bookingRepository.IsReturningCustomer(customerEmail);
                decimal finalPrice = price;
                if (isReturningCustomer)
                {
                    // Apply a 10% discount for returning customers
                    finalPrice *= 0.9m; // 10% discount
                }

                var booking = new Domain.Entities.Booking
                {
                    Date = slot.TimeSlot,
                    SlotId = slotId,
                    CustomerEmail = customerEmail,
                    Price = finalPrice
                };

                await _bookingRepository.AddAsync(booking);

                return BookingConfirmationResponse.SuccessResponse(slot.TimeSlot, finalPrice, customerEmail, isReturningCustomer);
            }
            else
            {
                return BookingConfirmationResponse.Failed("Booking could not be confirmed");
            }
        }
    }
}
