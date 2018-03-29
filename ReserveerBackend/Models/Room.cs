using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        //public Item Items { get; set; }

        public List<Item> Items { get; set; }

        public int Temperature { get; set; }

        public DateTime TemperatureDateTime { get; set; }
    }
}
