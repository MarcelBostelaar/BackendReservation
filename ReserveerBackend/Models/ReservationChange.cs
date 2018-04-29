using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class ReservationChange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Room Room { get; set; }
        [Required]
        public Reservation Reservation { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public DateTime OldStatDate { get; set; }
        [Required]
        public DateTime OldEndDate { get; set; }
        [Required]
        public bool OldActive { get; set; }
        [Required]
        public DateTime ChangeDate { get; set; }
    }
}
