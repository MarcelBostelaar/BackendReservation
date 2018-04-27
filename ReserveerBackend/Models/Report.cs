using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Report
    {
        [Key]
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
        [Required]
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
