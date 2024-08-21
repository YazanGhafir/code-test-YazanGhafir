using Hedin.ChangeTires.Modules.Booking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces
{
    public interface IBookingLister
    {
        public Task<List<BookingDto>> AllBookingsAsync(string customerEmail);
    }
}
