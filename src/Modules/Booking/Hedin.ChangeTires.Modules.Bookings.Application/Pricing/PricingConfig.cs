using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Hedin.ChangeTires.Modules.Booking.Application.Pricing
{
    public class PricingConfig
    {
        public BasePrice BasePrices { get; set; }
        public TireSizePrice TireSizePrices { get; set; }
        public ServicePrice ServicePrices { get; set; }
        public TireSizeLimits TireSizeLimits { get; set; }

        public static PricingConfig LoadFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<PricingConfig>(jsonString);
        }

        public static PricingConfig LoadFromApi(string url)
        {
            throw new NotImplementedException();
        }
    }

    public class BasePrice
    {
        public decimal Sedan { get; set; }
        public decimal SUV { get; set; }
        public decimal Truck { get; set; }
        public decimal Other { get; set; }
    }

    public class TireSizePrice
    {
        public decimal Small { get; set; }
        public decimal Medium { get; set; }
        public decimal Large { get; set; }
    }

    public class ServicePrice
    {
        public decimal WheelAlignment { get; set; }
        public decimal Balancing { get; set; }
    }

    public class TireSizeLimits
    {
        public int SmallUpperLimit { get; set; }
        public int MediumUpperLimit { get; set; }
    }
}
