using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Dtos
{
    public class BookingConfirmationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? Price { get; set; }
        public string CustomerEmail { get; set; }
        public bool LoyaltyDiscountApplied { get; set; }

        public static BookingConfirmationResponse Failed(string message)
        {
            return new BookingConfirmationResponse { Success = false, Message = message };
        }

        public static BookingConfirmationResponse SuccessResponse(DateTime bookingDate, decimal price, string customerEmail, bool loyaltyDiscountApplied)
        {
            return new BookingConfirmationResponse
            {
                Success = true,
                Message = "Booking confirmed" + (loyaltyDiscountApplied ? " with a 10% loyalty discount." : "."),
                BookingDate = bookingDate,
                Price = price,
                CustomerEmail = customerEmail,
                LoyaltyDiscountApplied = loyaltyDiscountApplied
            };
        }
    }
}
