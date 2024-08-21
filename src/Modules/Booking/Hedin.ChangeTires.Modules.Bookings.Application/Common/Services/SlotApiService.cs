using Hedin.ChangeTires.BuildingBlocks.Infrastructure.Configurations;
using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Hedin.ChangeTires.Modules.Booking.Application.Dtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Services
{
    public class SlotApiService : ISlotApiService
    {
        private readonly SlotApiSettings _settings;
        private readonly HttpClient _httpClient;

        public SlotApiService(IOptions<SlotApiSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = new HttpClient();
        }

        public async Task<List<SlotResponseDto>> GetAvailableSlotsAsync()
        {
            var response = await _httpClient.GetAsync(new Uri(GetBaseUrl() + "/" + "available"));
            response.EnsureSuccessStatusCode();

            var slots = await response.Content.ReadFromJsonAsync<List<SlotResponseDto>>();
            return slots ?? new List<SlotResponseDto>();
        }

        public async Task<SlotResponseDto> GetSlotAsync(Guid slotId)
        {

            var response = await _httpClient.GetAsync(new Uri(GetBaseUrl() + "/" + slotId.ToString()));
            response.EnsureSuccessStatusCode();

            var slot = await response.Content.ReadFromJsonAsync<SlotResponseDto>();
            return slot;
        }



        public async Task<bool> ConfirmBookingAsync(Guid slotId)
        {
            var response = await _httpClient.PutAsJsonAsync(new Uri(GetBaseUrl() + "/" + slotId), "");
            return response.IsSuccessStatusCode;
        }



        public async Task<bool> UnbookSlotAsync(Guid slotId)
        {
            var response = await _httpClient.DeleteAsync(new Uri(GetBaseUrl() + "/" + slotId));
            return response.IsSuccessStatusCode;
        }

        private string GetBaseUrl()
        {
            return _settings.Url + "/" + _settings.TenantId + "/slots";
        }
    }
}
