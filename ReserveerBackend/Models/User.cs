using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Boolean EmailNotification { get; set; }
        public UserPasswordLogin PasswordLogin { get; set; }

        public List<Report> Reports { get; set; }
        public List<Participant> Participants { get; set; }
        public List<ParticipantChange> ParticipantChanges { get; set; }
    }

    public enum Role
    {
        Student = 0,
        Teacher = 1,
        ServiceDesk = 2,
        Admin = 3
    }
}
