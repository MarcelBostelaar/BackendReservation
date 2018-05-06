using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Report
    {
        public Report() { }
        public Report(Room room, User user, string Description)
        {
            this.Room = room;
            this.User = user;
            this.Description = Description;
            this.ActionStatus = ActionStatus.Open;
        }
        public Report(Room room, User user, string Description, Reservation reservation) : this(room, user, Description)
        {
            this.Reservation = reservation;
        }
        public Report(Room room, User user, string Description, Reservation reservation, DateTime requestedEndTime) : this(room, user, Description, reservation)
        {
            this.RequestedEndTime = requestedEndTime;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Room Room { get; set; }
        //Not required
        public Reservation Reservation { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ActionStatus ActionStatus { get; set; }
        
        public DateTime RequestedEndTime { get; set; }
    }
    public enum ActionStatus
    {
        Open = 1,
        BeingProccessed = 2,
        Solved = 3,
        Dismissed = 4,
        Cancelled = 5
    }
}
