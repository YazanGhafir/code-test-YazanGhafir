using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hedin.ChangeTires.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlotController : ControllerBase
    {
        private readonly ISlotApiService _slotApiService;
        private readonly ILogger<SlotController> _logger;

        public SlotController(ISlotApiService slotApiService, ILogger<SlotController> logger)
        {
            this._slotApiService = slotApiService;
            _logger = logger;
        }

        [HttpGet("getAvailableSlots")]
        public async Task<IActionResult> GetAvailableSlots()
        {
            _logger.LogInformation("Getting available slots");
            var slots = await _slotApiService.GetAvailableSlotsAsync();
            return Ok(slots);
        }
    }
}
