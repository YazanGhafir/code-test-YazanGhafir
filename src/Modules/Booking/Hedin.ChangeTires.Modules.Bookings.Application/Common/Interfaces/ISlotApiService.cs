using Hedin.ChangeTires.Modules.Booking.Application.Dtos;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces
{
    public interface ISlotApiService
    {
        Task<bool> ConfirmBookingAsync(Guid slotId);
        Task<List<SlotResponseDto>> GetAvailableSlotsAsync();
        Task<SlotResponseDto> GetSlotAsync(Guid slotId);
        Task<bool> UnbookSlotAsync(Guid slotId);
    }
}