using Hedin.ChangeTires.Modules.Booking.Application.Dtos;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces
{
    public interface IBooker
    {
        Task<BookingConfirmationResponse> BookTireChangeAsync(Guid slotId, string customerEmail, decimal price);
    }
}