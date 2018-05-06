using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Participant
    {
        public Participant() { }
        public Participant(Reservation reservation, User user, bool IsOwner, DateTime added)
        {
            this.Reservation = reservation;
            this.User = user;
            this.IsOwner = IsOwner;
            this.UserID = user.Id;
            this.ReservationID = reservation.Id;
            this.Added = added;
        }

        [Required]
        public Reservation Reservation { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public DateTime Added { get; set; }

        public int UserID { get; set; }
        public int ReservationID { get; set; }
        [Required]
        public bool IsOwner { get; set; }

        public ParticipantChange GenerateChangeCopy(DateTime changedate)
        {
            var i = new ParticipantChange();
            i.Added = Added;
            i.ChangeDate = changedate;
            i.OldIsOwner = IsOwner;
            i.Reservation = Reservation;
            i.ReservationID = ReservationID;
            i.User = User;
            i.UserID = UserID;
            return i;
        }
    }
}
