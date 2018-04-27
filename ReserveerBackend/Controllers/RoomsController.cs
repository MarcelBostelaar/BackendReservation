using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveerBackend.Models;

namespace ReserveerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Rooms")]
    public class RoomsController : Controller
    {
        ReserveerDBContext dbContext;
        public RoomsController(ReserveerDBContext context)
        {
            dbContext = context;
        }


        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return dbContext.Rooms.ToList();
        }
    }
}