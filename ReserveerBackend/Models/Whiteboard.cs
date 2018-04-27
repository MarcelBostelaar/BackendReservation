using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Whiteboard
    {
        [Key]
        public int Id { get; set; }

        public Room Room { get; set; }
    }
}
