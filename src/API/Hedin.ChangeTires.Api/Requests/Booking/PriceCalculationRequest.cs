using Hedin.ChangeTires.Modules.Booking.Domain.Values;

namespace Hedin.ChangeTires.Api.Requests.Booking
{
    public class PriceCalculationRequest : ApiRequest
    {
        public CarType CarType { get; set; }
        public int TireSize { get; set; }
        public bool IncludeWheelAlignment { get; set; }
        public bool IncludeBalancing { get; set; }
        public DateTime TimeSlot { get; set; }
    }

}
