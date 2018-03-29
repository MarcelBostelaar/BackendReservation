﻿using System;
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

        public string Role { get; set; }

        public string Email { get; set; }

        public Boolean EmailNotification { get; set; }
    }
}
