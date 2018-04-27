using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Boolean EmailNotification { get; set; }

        public List<Report> Reports { get; set; }
    }
}
