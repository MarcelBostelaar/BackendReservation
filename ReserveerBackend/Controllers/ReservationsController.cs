using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReserveerBackend;
using ReserveerBackend.Models;

namespace ReserveerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Reservations")]
    [Authorize(Roles = Authorization.StudentOrHigher)]
    public class ReservationsController : Controller
    {
        private readonly ReserveerDBContext _context;
        private readonly object ReservationLock = new object();

        public ReservationsController(ReserveerDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddReservation(DateTime StartTime, DateTime EndTime, string Description, int RoomID, HashSet<int> Participants, int Room)
        {
            if (StartTime == null || EndTime == null || Description == null)
            {
                Response.StatusCode = 400;
                return Content("Fields missing");
            }
            if (StartTime > EndTime)
            {
                Response.StatusCode = 400;
                return Content("StartTime is before EndTime");
            }
            var room = _context.Rooms.Find(Room);
            if (room == null)
            {
                Response.StatusCode = 400;
                return Content("Room does not exist");
            }

            if (Participants == null)
                Participants = new HashSet<int>();

            var requesterID = int.Parse(User.FindFirst(Models.User._ID).Value);
            var _participants = from x in Participants select _context.Users.Find(x);

            if (!_participants.All(x => x != null))
            {
                Response.StatusCode = 400;
                return Content("A participant could not be found.");
            }
            var _owner = _context.Users.Find(requesterID);

            lock (ReservationLock) //lock all intersection checking and reservation writing logic to prevent changes to the database during the checking phase
            {
                var intersections = FindIntersections(StartTime, EndTime);
                if (intersections.Count() == 0) //No intersections with other reservations, add it
                {
                    return createreservation(StartTime, EndTime, Description, _owner, _participants, room);
                }
                if (!intersections.All(x => x.IsMutable == true)) //intersection with an immutable reservation
                {
                    Response.StatusCode = 400;
                    return Content("Overlaps with an immutable reservation.");
                }
                if (Authorization.AIsBOrHigher(_owner.Role, Role.ServiceDesk)) //service desk or higher can always forcibly add reservations
                {
                    return forceAddReservation(StartTime, EndTime, Description, _owner, _participants, room);
                }
                var _intersectionowners = (from x in intersections select x.Participants).SelectMany(x => x).Where(x => x.IsOwner).Select(x => x.UserID);
                var _intersectionownerLevels = (from x in _intersectionowners select _context.Users.Find(x).Role);
                if (!_intersectionownerLevels.All(x => Authorization.AIsHigherThanB(_owner.Role, x))) //intersections with reservation from people of higher or equal level
                {
                    Response.StatusCode = 400;
                    return Content("Overlaps with a reservation with owner of equal level.");
                }
                return forceAddReservation(StartTime, EndTime, Description, _owner, _participants, room); //intersections with reservations from people with lower level
            }
        }

        private IActionResult forceAddReservation(DateTime StartTime, DateTime EndTime, string Description, User Owner, IEnumerable<User> Participants, Room Room, bool IsMutable = true)
        {
            throw new NotImplementedException();
        }

        private IActionResult createreservation(DateTime StartTime, DateTime EndTime, string Description, User Owner, IEnumerable<User> Participants, Room Room, bool IsMutable = true)
        {
            Participants = Participants.Where(x => x.Id != Owner.Id);

            var reservation = new Reservation();
            reservation.Active = true;
            reservation.Description = Description;
            reservation.StartDate = StartTime;
            reservation.EndDate = EndTime;
            reservation.IsMutable = IsMutable;
            reservation.Room = Room;

            var participantlist = (from x in Participants select new Participant(reservation, x, false)).ToList();
            participantlist.Add(new Participant(reservation, Owner, true));
            reservation.Participants = participantlist;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Content("Successfully added reservation");
        }

        private IEnumerable<Reservation> FindIntersections(DateTime StartTime, DateTime EndTime)
        {
            if (StartTime > EndTime)
                throw new Exception("Starttime is later than endtime");
            return _context.Reservations.AsQueryable().Where(x => !(x.StartDate > EndTime || x.EndDate < StartTime)).Include(x => x.IsMutable).Include(x => x.Participants);
        }
    }
}
