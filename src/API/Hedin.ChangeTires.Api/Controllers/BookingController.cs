using Hedin.ChangeTires.Api.Requests.Booking;
using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Hedin.ChangeTires.Modules.Booking.Application.Common.Services;
using Hedin.ChangeTires.Modules.Booking.Application.Dtos;
using Hedin.ChangeTires.Modules.Booking.Domain.Values;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Hedin.ChangeTires.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBooker _booker;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IBookingLister _bookingLister;
        private readonly ISlotApiService _slotApiService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBooker booker, IPriceCalculator priceCalculator, IBookingLister bookingLister, ISlotApiService slotApiService, ILogger<BookingController> logger)
        {
            _booker = booker;
            _priceCalculator = priceCalculator;
            _bookingLister = bookingLister;
            _slotApiService = slotApiService;
            _logger = logger;
        }

        [HttpPost(nameof(CreateNewBooking))]
        public async Task<IActionResult> CreateNewBooking([FromQuery] BookingRequest request)
        {
            try
            {
                _logger.BeginScope("Creating booking for {CustomerEmail} at {TimeSlot}", request.CustomerEmail, request.TimeSlot);

                var slot = await _slotApiService.GetSlotAsync(request.SlotId);

                // validate the time slot by initializing it 
                request.TimeSlot = slot.TimeSlot;

                var price = _priceCalculator.CalculatePrice(request.CarType, request.TireSize,
                    request.IncludeWheelAlignment, request.IncludeBalancing);

                var result = await _booker.BookTireChangeAsync(request.SlotId, request.CustomerEmail, price);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid booking request");
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                string message = "Slot not found";
                _logger.LogError(ex, message);
                return StatusCode(500, message);
            }
            catch (Exception ex)
            {
                string message = "An error occurred while processing the booking request";
                _logger.LogError(ex, message);
                return StatusCode(500, message);
            }
        }

        [HttpGet(nameof(GetCalculatedPrice))]
        public IActionResult GetCalculatedPrice([FromQuery] PriceCalculationRequest request)
        {
            try
            {
                _logger.BeginScope("Calculating price for {CarType} with tire size {TireSize}", request.CarType, request.TireSize);

                var price = _priceCalculator.CalculatePrice(request.CarType, request.TireSize,
                request.IncludeWheelAlignment, request.IncludeBalancing);

                return Ok(price);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid price calculation request");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                string message = "An error occurred while processing your request";
                _logger.LogError(ex, message);
                return StatusCode(500, message);
            }
        }

        [HttpPost(nameof(GetAllBookings))]
        public async Task<List<BookingDto>> GetAllBookings(string customerEmail)
        {
            _logger.BeginScope("Retrieving all bookings for {CustomerEmail}", customerEmail);

            var bookings = await _bookingLister.AllBookingsAsync(customerEmail);
            return bookings;
        }
    }
}
