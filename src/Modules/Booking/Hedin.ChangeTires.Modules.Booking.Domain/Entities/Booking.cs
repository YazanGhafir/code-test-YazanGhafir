using Hedin.ChangeTires.BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.ChangeTires.Modules.Booking.Domain.Entities
{
    public class Booking : Entity
    {
        [Key]
        public int Id { get; set; }

        public string CustomerEmail { get; set; }
        public DateTime Date { get; set; }
        public Guid SlotId { get; set; }
        public decimal Price { get; set; }

        // Foreign key for Car
        public string RegistrationNumber { get; set; }

        // Navigation property for Car
        [ForeignKey("RegistrationNumber")]
        public Car Car { get; set; }
    }
}
