using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class ParticipantChange
    {
        [Required]
        public Reservation Reservation { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public bool OldIsOwner { get; set; }
        [Required]
        public DateTime ChangeDate { get; set; }
        [Required]
        public DateTime Added { get; set; }

        public int UserID { get; set; }
        public int ReservationID { get; set; }
    }
}
