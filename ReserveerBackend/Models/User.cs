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
        int Id { get; set; }

        public string UserName { get; set; }
    }
}
