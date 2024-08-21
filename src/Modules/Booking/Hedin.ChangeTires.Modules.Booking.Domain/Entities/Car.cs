using Hedin.ChangeTires.BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Domain.Entities
{
    public class Car : Entity
    {
        [Key]
        public string RegistrationNumber { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int TireSize { get; set; }
    }
}
