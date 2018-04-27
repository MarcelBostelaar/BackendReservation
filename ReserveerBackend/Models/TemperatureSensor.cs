using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class TemperatureSensor
    {
        [Key]
        public int Id { get; set; }
        public List<Temperature> Temperatures { get; set; }
    }
}
