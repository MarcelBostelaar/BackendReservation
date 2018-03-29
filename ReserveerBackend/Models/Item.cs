using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string ItemName { get; set; }

        public Boolean Functional { get; set; }


    }
}
