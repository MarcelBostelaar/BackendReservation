using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public bool IsMutable { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Room Room { get; set; }

        public List<Report> Reports { get; set; }
    }
}
