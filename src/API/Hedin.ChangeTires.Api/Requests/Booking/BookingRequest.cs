using Hedin.ChangeTires.Modules.Booking.Domain.Values;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Hedin.ChangeTires.Api.Requests.Booking
{
    public class BookingRequest : ApiRequest
    {
        public Guid SlotId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CarType CarType { get; set; }
        public int TireSize { get; set; }
        public bool IncludeWheelAlignment { get; set; }
        public bool IncludeBalancing { get; set; }


        private string _customerEmail;
        public string CustomerEmail
        {
            get => _customerEmail;
            set
            {
                // Regex pattern for validating email address format
                string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
                if (Regex.IsMatch(value, pattern))
                {
                    _customerEmail = value;
                }
                else
                {
                    throw new ArgumentException("Invalid email format.");
                }
            }
        }

        private DateTime _timeSlot;
        internal DateTime TimeSlot
        {
            get => _timeSlot;
            set
            {
                // check that the time slot is at least 24 hours in advance and not more than 30 days ahead.
                if (value < DateTime.Now.AddHours(24) || value > DateTime.Now.AddDays(30))
                {
                    throw new ArgumentException("Time slot must be at least 24 hours in advance and not more than 30 days ahead.");
                }

                // validate operating hours
                var dayOfWeek = value.DayOfWeek;
                var timeOfDay = value.TimeOfDay;

                // monday to Friday: 8:00 AM to 5:00 PM
                if (dayOfWeek >= DayOfWeek.Monday && dayOfWeek <= DayOfWeek.Friday)
                {
                    if (timeOfDay < new TimeSpan(8, 0, 0) || timeOfDay > new TimeSpan(17, 0, 0)) // 5:00 PM is exclusive
                    {
                        throw new ArgumentException("Booking time is outside operating hours.");
                    }
                }
                // saturday: 9:00 AM to 2:00 PM
                else if (dayOfWeek == DayOfWeek.Saturday)
                {
                    if (timeOfDay < new TimeSpan(9, 0, 0) || timeOfDay > new TimeSpan(14, 0, 0)) // 2:00 PM is exclusive
                    {
                        throw new ArgumentException("Booking time is outside operating hours.");
                    }
                }
                // sunday: Closed
                else
                {
                    throw new ArgumentException("Bookings are not permitted on Sundays.");
                }

                _timeSlot = value;
            }
        }

    }
}
