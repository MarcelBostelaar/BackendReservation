using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public bool TV { get; set; }
        [Required]
        public bool Smartboard { get; set; }
        [Required]
        public int Powersupply { get; set; }

        public List<Reservation> Reservations { get; set; }
        public List<Report> Reports { get; set; }
        public List<Temperature> Temperatures { get; set; }
        public List<Whiteboard> Whiteboards { get; set; }
    }
}
