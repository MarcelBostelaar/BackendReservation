using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class UserPasswordLogin
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public byte[] HashedPassword { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        [Required]
        public User User { get; set; }
    }
}
