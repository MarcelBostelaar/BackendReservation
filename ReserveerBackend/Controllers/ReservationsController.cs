using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReserveerBackend;
using ReserveerBackend.MessagingSystem;
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
        [Route("get")]
        public IEnumerable<Reservation> GetMatch(int? Id, bool? isactive, bool? ismutable, string description, int? roomID, DateTime? after, DateTime? before)
        {
            var validReservations = _context.Reservations.AsQueryable();
            if (Id.HasValue)
                return validReservations.Where(x => x.Id == Id.Value); //only one match possible
            if (isactive.HasValue)
                validReservations = validReservations.Where(x => x.Active == isactive.Value);
            if (ismutable.HasValue)
                validReservations = validReservations.Where(x => x.IsMutable == ismutable.Value);
            if (description != null)
                validReservations = validReservations.Where(x => x.Description.Contains(description));
            if (roomID.HasValue)
                validReservations = validReservations.Where(x => x.Room.Id == roomID);
            if (after.HasValue)
                validReservations = validReservations.Where(x => x.EndDate >= after.Value);
            if (before.HasValue)
                validReservations = validReservations.Where(x => x.StartDate <= before.Value);
            return validReservations;
        }

        [HttpPost]
        [Route("Participants/Add")]
        public IActionResult AddParticipants(List<Tuple<int, bool>> UserIds, int reservationid)
        {
            //Check if user is owner or service desk member or higher
            var owner = Models.User.FromClaims(User.Claims);
            var _reservation = _context.Reservations.Where(x => x.Id == reservationid).Include(x=>x.Participants).Include(x=>x.ParticipantChanges);
            if(_reservation.Count() != 1)
            {
                Response.StatusCode = 400;
                return Content("Could not find reservation");
            }
            var reservation = _reservation.First();
            if(reservation.Participants.Where(x=> x.UserID == owner.Id).Where(x => x.IsOwner).Count() != 1 || Authorization.AIsBOrHigher(owner.Role, Role.ServiceDesk))
            {
                Response.StatusCode = 401;
                return Content("You are not the owner of this reservation, nor are you a service desk member or higher");
            }

            if (UserIds == null)
            {
                Response.StatusCode = 400;
                return Content("UserId's cannot be empty");
            }
            var users = from id in UserIds select new Tuple<User, bool>(_context.Users.Find(id.Item1), id.Item2);
            if(!users.All(x => x.Item1 != null))
            {
                Response.StatusCode = 400;
                return Content("A user could not be found");
            }

            foreach (var user in users)
            {
                InviteUser(owner, user.Item1, user.Item2, reservation);
            }
            return Content("Succesfull added or updated users");
        }

        [HttpDelete]
        [Route("Participants/Remove")]
        public IActionResult RemoveParticipants(List<int> UserIds, int reservationid)
        {
            var owner = Models.User.FromClaims(User.Claims);
            var _reservation = _context.Reservations.Where(x => x.Id == reservationid).Include(x => x.Participants).Include(x => x.ParticipantChanges);
            if (_reservation.Count() != 1)
            {
                Response.StatusCode = 400;
                return Content("Could not find reservation");
            }
            var reservation = _reservation.First();
            if (reservation.Participants.Where(x => x.UserID == owner.Id).Where(x => x.IsOwner).Count() != 1 || Authorization.AIsBOrHigher(owner.Role, Role.ServiceDesk))
            {
                Response.StatusCode = 401;
                return Content("You are not the owner of this reservation, nor are you a service desk member or higher");
            }

            if (UserIds == null)
            {
                Response.StatusCode = 400;
                return Content("UserId's cannot be empty");
            }
            var users = from id in UserIds select _context.Users.Find(id);
            if (!users.All(x => x != null))
            {
                Response.StatusCode = 400;
                return Content("A user could not be found");
            }

            foreach (var user in users)
            {
                RemoveUser(owner, user, reservation);
            }
            _context.SaveChanges();
            return Ok(users.ToList());
        }

        private void InviteUser(User actor, User target, bool asOwner, Reservation reservation)
        {
            Debug.WriteLine("Should have invited user, but added as participant instead. Please fix 'InviteUser' in 'ReservationController.cs' if the messaging and invitation system is implemented.");
            var participant = reservation.Participants.Find(x => x.UserID == target.Id);
            if (participant == null)
            {
                reservation.Participants.Add(new Participant(reservation, target, asOwner, DateTime.Now));
                _context.SaveChanges();
                return;
            }
            else
            {
                var participantchange = participant.GenerateChangeCopy(DateTime.Now);
                participant.IsOwner = asOwner;
                _context.ParticipantChanges.Add(participantchange);
                _context.Participants.Add(participant);
                _context.SaveChanges();
                return;
            }
        }

        private void RemoveUser(User actor, User target, Reservation reservation)
        {
            Debug.WriteLine("Should have messaged user, but removed as participant instead. Please fix 'RemoveUser' in 'ReservationController.cs' if the messaging system is implemented.");
            var participant = reservation.Participants.Find(x => x.UserID == target.Id);
            if (participant == null)//user is not a participant
                return;
            _context.ParticipantChanges.Add(participant.GenerateChangeCopy(DateTime.Now));
            reservation.Participants.Remove(participant);
        }

        private bool CanEditReservation(Reservation reservation, User actor)
        {
            if (reservation.Participants.Where(x => x.IsOwner).Where(x => x.UserID == actor.Id).Count() != 1)
            {
                if (!Authorization.AIsBOrHigher(actor.Role, Role.ServiceDesk))
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost]
        [Route("Change")]
        public IActionResult ChangeReservation(int ReservationID, DateTime? StartTime, DateTime? EndTime, string Description, int? RoomID, bool? isactive, bool Force = false)
        {
            User actor = Models.User.FromClaims(User.Claims);
            Room room = null;
            if (RoomID.HasValue)
            {
                room = _context.Rooms.Find(RoomID.Value);
                if (room == null)
                {
                    Response.StatusCode = 400;
                    return Content("RoomID could not be found");
                }
            }
            lock (ReservationLock)
            {
                var _reservation = _context.Reservations.Where(x => x.Id == ReservationID).Include(x => x.Participants);
                Reservation reservation = null;
                if(_reservation.Count() != 0)
                {
                    Response.StatusCode = 400;
                    return Content("Reservation could not be found");
                }
                reservation = _reservation.First();

                if(reservation.Participants.Where(x => x.IsOwner).Where(x => x.UserID == actor.Id).Count() != 1)
                {
                    if(!Authorization.AIsBOrHigher(actor.Role, Role.ServiceDesk))
                    {
                        Response.StatusCode = 401;
                        return Content("You are not an owner of this reservation, nor are you a service desk employee or higher.");
                    }
                }

                var reservationchange = reservation.GenerateChangeCopy(actor);

                DateTime start;
                if (StartTime.HasValue)
                    start = StartTime.Value;
                else
                    start = reservation.StartDate;
                DateTime end;
                if (EndTime.HasValue)
                    end = EndTime.Value;
                else
                    end = reservation.EndDate;

                if(start > end)
                {
                    Response.StatusCode = 400;
                    return Content("Startdate cannot come before end date");
                }

                var intersections = FindIntersections(start, end);
                if (intersections.Count() <= 1)
                {
                    return _ChangeReservation(isactive, reservation, start, end, room, Description, reservationchange);
                }
                else
                {
                    intersections = intersections.Where(x => x.Id != reservation.Id); //remove self
                    if (!Force)
                    {
                        Response.StatusCode = 400;
                        return Content("There is overlap with existing reservations, please set 'Force' to true in your request if you wish to forcibly insert it.");
                    }
                    if (Authorization.AIsBOrHigher(actor.Role, Role.ServiceDesk)) //service desk or higher can always forcibly change reservations
                    {
                        return _ForceChangeReservation(isactive, reservation, start, end, room, Description, reservationchange, actor, intersections);
                    }
                    var _intersectionowners = (from x in intersections select x.Participants).SelectMany(x => x).Where(x => x.IsOwner).Select(x => x.UserID);
                    var _intersectionownerLevels = (from x in _intersectionowners select _context.Users.Find(x).Role);
                    if (!_intersectionownerLevels.All(x => Authorization.AIsHigherThanB(actor.Role, x))) //intersections with reservation from people of higher or equal level
                    {
                        Response.StatusCode = 400;
                        return Content("Overlaps with a reservation with owner of equal or higher level.");
                    }
                    return _ForceChangeReservation(isactive, reservation, start, end, room, Description, reservationchange, actor, intersections); //intersections with reservations from people with lower level
                }
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddReservation(DateTime StartTime, DateTime EndTime, string Description, int RoomID, bool Force = false)
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
            var room = _context.Rooms.Find(RoomID);
            if (room == null)
            {
                Response.StatusCode = 400;
                return Content("Room does not exist");
            }
            var Owner = Models.User.FromClaims(User.Claims);

            lock (ReservationLock) //lock all intersection checking and reservation writing logic to prevent changes to the database during the checking phase
            {
                var intersections = FindIntersections(StartTime, EndTime);
                if (intersections.Count() == 0) //No intersections with other reservations, add it
                {
                    return Content(createreservation(StartTime, EndTime, Description, Owner, room).ToString());
                }
                if (!intersections.All(x => x.IsMutable == true)) //intersection with an immutable reservation
                {
                    Response.StatusCode = 400;
                    return Content("Overlaps with an immutable reservation.");
                }
                if (!Force)
                {
                    Response.StatusCode = 400;
                    return Content("There is overlap with existing reservations, please set 'Force' to true in your request if you wish to forcibly insert it.");
                }
                if (Authorization.AIsBOrHigher(Owner.Role, Role.ServiceDesk)) //service desk or higher can always forcibly add reservations
                {
                    return Content(OverrideAddReservation(intersections, StartTime, EndTime, Description, Owner, room).ToString());
                }
                var _intersectionowners = (from x in intersections select x.Participants).SelectMany(x => x).Where(x => x.IsOwner).Select(x => x.UserID);
                var _intersectionownerLevels = (from x in _intersectionowners select _context.Users.Find(x).Role);
                if (!_intersectionownerLevels.All(x => Authorization.AIsHigherThanB(Owner.Role, x))) //intersections with reservation from people of higher or equal level
                {
                    Response.StatusCode = 400;
                    return Content("Overlaps with a reservation with owner of equal or higher level.");
                }
                return Content(OverrideAddReservation(intersections, StartTime, EndTime, Description, Owner, room).ToString()); //intersections with reservations from people with lower level
            }
        }

        private IActionResult _ForceChangeReservation(bool? isactive, Reservation reservation, DateTime start, DateTime end, Room room, string Description, ReservationChange reservationchange, User actor, IEnumerable<Reservation> Intersections)
        {
            if (!Intersections.All(x => x.IsMutable))
            {
                throw new Exception("Cannot forcibly override immutable reservation");
            }
            foreach (var intersection in Intersections)
            {
                ShortenOtherReservation(start, end, intersection, actor);
            }
            return _ChangeReservation(isactive, reservation, start, end, room, Description, reservationchange);
        }

        private IActionResult _ChangeReservation(bool? isactive, Reservation reservation, DateTime start, DateTime end, Room room, string Description, ReservationChange reservationchange)
        {
            if (isactive.HasValue)
                reservation.Active = isactive.Value;
            reservation.StartDate = start;
            reservation.EndDate = end;
            if (room != null)
                reservation.Room = room;
            if (Description != null)
                reservation.Description = Description;
            _context.Add(reservation);
            _context.ReservationChanges.Add(reservationchange);
            _context.SaveChanges();
            return Content("Succesfully changed reservation");
        }

        private int OverrideAddReservation(IEnumerable<Reservation> Intersections, DateTime StartTime, DateTime EndTime, string Description, User Owner, Room Room, bool IsMutable = true)
        {
            if(!Intersections.All(x => x.IsMutable))
            {
                throw new Exception("Cannot forcibly override immutable reservation");
            }
            foreach (var intersection in Intersections)
            {
                ShortenOtherReservation(StartTime, EndTime, intersection, Owner);
            }
            var reservationid = createreservation(StartTime, EndTime, Description, Owner, Room);
            _context.SaveChanges();
            
            return reservationid;
        }

        private void ShortenOtherReservation(DateTime StartTime, DateTime EndTime, Reservation OtherReservation, User actor)
        {
            var oldreservation = OtherReservation.GenerateChangeCopy(actor);
            if(OtherReservation.EndDate > StartTime)
                OtherReservation.EndDate = StartTime;
            if (OtherReservation.StartDate < EndTime)
                OtherReservation.StartDate = EndTime;

            _context.Reservations.Add(OtherReservation);
            _context.ReservationChanges.Add(oldreservation);

            foreach (var participant in OtherReservation.Participants)
            {
                Notifications.SendReservationChangedMessage(participant.User, actor, OtherReservation, oldreservation);
            }
        }

        private int createreservation(DateTime StartTime, DateTime EndTime, string Description, User Owner, Room Room, bool IsMutable = true)
        {
            var reservation = new Reservation();
            reservation.Active = true;
            reservation.Description = Description;
            reservation.StartDate = StartTime;
            reservation.EndDate = EndTime;
            reservation.IsMutable = IsMutable;
            reservation.Room = Room;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation.Id;
        }

        private IEnumerable<Reservation> FindIntersections(DateTime StartTime, DateTime EndTime)
        {
            if (StartTime > EndTime)
                throw new Exception("Starttime is later than endtime");
            return _context.Reservations.AsQueryable().Where(x => !(x.StartDate >= EndTime || x.EndDate <= StartTime)).Include(x => x.Participants);
        }
    }
}
