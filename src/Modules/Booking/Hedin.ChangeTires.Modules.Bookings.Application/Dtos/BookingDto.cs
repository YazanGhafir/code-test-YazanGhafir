using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime Date { get; set; }
        public Guid SlotId { get; set; }
        public decimal Price { get; set; }
        public CarDto CarDetails { get; set; }
    }

}
