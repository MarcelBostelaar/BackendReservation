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
        ReserveerDBContext _context;
        public RoomsController(ReserveerDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IEnumerable<Room> GetMatch(int? Id, string Name, string Location, int? MinCapacity, int? MaxCapacity, bool? TV, bool? Smartboard, int? MinPowersupply, int? MaxPowersupply)
        {
            var validrooms = _context.Rooms.AsQueryable();
            if (Id.HasValue)
                return validrooms.Where(x => x.Id == Id.Value); //only one match possible
            if (Name != null)
                validrooms = validrooms.Where(x => x.Name.Contains(Name));
            if(Location != null)
                validrooms = validrooms.Where(x => x.Location.Contains(Location));
            if (MinCapacity.HasValue)
                validrooms = validrooms.Where(x => x.Capacity >= MinCapacity.Value);
            if (MaxCapacity.HasValue)
                validrooms = validrooms.Where(x => x.Capacity <= MaxCapacity.Value);
            if (TV.HasValue)
                validrooms = validrooms.Where(x => x.TV == TV.Value);
            if (Smartboard.HasValue)
                validrooms = validrooms.Where(x => x.Smartboard == Smartboard.Value);
            if (MinPowersupply.HasValue)
                validrooms = validrooms.Where(x => x.Powersupply >= MinPowersupply.Value);
            if (MaxPowersupply.HasValue)
                validrooms = validrooms.Where(x => x.Powersupply <= MaxPowersupply.Value);
            return validrooms;
        }


    }
}