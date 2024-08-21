using Hedin.ChangeTires.Modules.Booking.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces
{
    public interface IPriceCalculator
    {
        decimal CalculatePrice(CarType carType, int tireSize, bool includeWheelAlignment, bool includeBalancing);
    }
}
