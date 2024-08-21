using Hedin.ChangeTires.BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Domain.Values
{
    internal class Price : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }
        public Price(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
