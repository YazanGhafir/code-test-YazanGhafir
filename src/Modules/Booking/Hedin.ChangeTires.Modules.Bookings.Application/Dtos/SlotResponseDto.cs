using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Dtos
{
    public class SlotResponseDto
    {
        public Guid Id { get; set; }
        public DateTime TimeSlot { get; set; }
    }
}
