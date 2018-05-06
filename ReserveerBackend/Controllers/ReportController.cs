using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReserveerBackend.Models;

namespace ReserveerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Report")]
    [Authorize(Roles = Authorization.StudentOrHigher)]
    public class ReportController : Controller
    {
        private readonly ReserveerDBContext _context;

        public ReportController(ReserveerDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = Authorization.ServiceOrHigher)]
        [Route("Get")]
        public IEnumerable<Report> GetMatch(int? Id, int? roomId, int? ReservationID, int? UserID, string description, ActionStatus? actionstatus)
        {
            IQueryable<Report> validReports = _context.Reports.AsQueryable().Include(x => x.Room).Include(x => x.User).Include(x => x.Reservation);
            if (Id.HasValue)
                return validReports.Where(x => x.Id == Id.Value);//only one match possible
            if (roomId.HasValue)
                validReports = validReports.Where(x => x.Room.Id == roomId.Value);
            if (ReservationID.HasValue)
                validReports = validReports.Where(x => x.Reservation.Id == ReservationID.Value);
            if (UserID.HasValue)
                validReports = validReports.Where(x => x.User.Id == UserID.Value);
            if (description != null)
                validReports = validReports.Where(x => x.Description.Contains(description));
            if (actionstatus.HasValue)
                validReports = validReports.Where(x => x.ActionStatus == actionstatus.Value);
            return validReports;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult createreport(int RoomID, string Description)
        {
            var Owner = Models.User.FromClaims(User.Claims);
            var room = _context.Rooms.Find(RoomID);
            if(room == null)
            {
                Response.StatusCode = 400;
                return Content("Room not found");
            }
            if (String.IsNullOrWhiteSpace(Description))
            {
                Response.StatusCode = 400;
                return Content("Description cannot be empty");
            }

            _context.Reports.Add(new Models.Report(room, Owner, Description));
            _context.SaveChanges();
            return Content("Succesfully added report");
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult createreportaboutreservation(int RoomID, string Description, int reservationid, DateTime? RequestedEndTime)
        {
            var Owner = Models.User.FromClaims(User.Claims);
            var room = _context.Rooms.Find(RoomID);
            var reservation = _context.Reservations.Find(reservationid);
            if (room == null)
            {
                Response.StatusCode = 400;
                return Content("Room not found");
            }
            if (reservation == null)
            {
                Response.StatusCode = 400;
                return Content("Reservation not found");
            }
            if (String.IsNullOrWhiteSpace(Description))
            {
                Response.StatusCode = 400;
                return Content("Description cannot be empty");
            }
            if (RequestedEndTime.HasValue)
                _context.Reports.Add(new Models.Report(room, Owner, Description, reservation, RequestedEndTime.Value));
            else
                _context.Reports.Add(new Models.Report(room, Owner, Description, reservation));
            _context.SaveChanges();
            return Content("Succesfully added report");
        }
    }
}