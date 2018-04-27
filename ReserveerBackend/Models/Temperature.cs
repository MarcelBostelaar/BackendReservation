using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Temperature
    {
        [Key]
        public TemperatureSensor Sensor { get; set; }
        [Key]
        public DateTime DateTime { get; set; }
        [Required]
        public float temperature { get; set; }
        [Required]
        public Room Room { get; set; }
    }
}
