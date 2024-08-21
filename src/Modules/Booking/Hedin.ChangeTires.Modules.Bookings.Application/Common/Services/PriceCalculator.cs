using Hedin.ChangeTires.Modules.Booking.Application.Common.Interfaces;
using Hedin.ChangeTires.Modules.Booking.Application.Pricing;
using Hedin.ChangeTires.Modules.Booking.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Application.Common.Services
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly PricingConfig _config;

        public PriceCalculator(PricingConfig config)
        {
            _config = config;
        }

        public decimal CalculatePrice(CarType carType, int tireSize, bool includeWheelAlignment, bool includeBalancing)
        {
            decimal price = CalculateBasePrice(carType);
            price += CalculateTireSizePrice(tireSize);
            price += CalculateServicePrice(includeWheelAlignment, includeBalancing);
            return price;
        }

        private decimal CalculateBasePrice(CarType carType)
        {
            return carType switch
            {
                CarType.Sedan => _config.BasePrices.Sedan,
                CarType.SUV => _config.BasePrices.SUV,
                CarType.Truck => _config.BasePrices.Truck,
                _ => _config.BasePrices.Other,
            };
        }

        private decimal CalculateTireSizePrice(int tireSize)
        {
            if (tireSize <= _config.TireSizeLimits.SmallUpperLimit)
            {
                return _config.TireSizePrices.Small;
            }
            else if (tireSize <= _config.TireSizeLimits.MediumUpperLimit)
            {
                return _config.TireSizePrices.Medium;
            }
            else
            {
                return _config.TireSizePrices.Large;
            }
        }

        private decimal CalculateServicePrice(bool includeWheelAlignment, bool includeBalancing)
        {
            decimal price = 0;
            if (includeWheelAlignment)
            {
                price += _config.ServicePrices.WheelAlignment;
            }
            if (includeBalancing)
            {
                price += _config.ServicePrices.Balancing;
            }
            return price;
        }
    }
}
