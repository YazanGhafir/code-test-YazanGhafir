using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Domain.Entities.Booking>> ListAllAsync(string customerEmail);
        Task AddAsync(Domain.Entities.Booking booking);
        Task<Domain.Entities.Booking> GetBySlotIdAsync(Guid slotId);
        Task<bool> IsReturningCustomer(string customerEmail);
    }
}
