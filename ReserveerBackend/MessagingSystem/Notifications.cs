using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.MessagingSystem
{
    public static class Notifications
    {
        public static void SendNotification(User receiver, User sender, string message)
        {
            Debug.WriteLine(string.Format("Should have send notification: \n{0}\nTo: {1}\nFrom: {2}", message, receiver.Id.ToString(), sender.Id.ToString()));
        }

        public static void SendReservationChangedMessage(User receiver, User sender, Reservation reservation, ReservationChange change)
        {
            SendNotification(receiver, sender, String.Format(
@"The reservation '{0}' was changed.
New time: {1} - {2}
Old time: {3} - {4}",
            reservation.Description,
            reservation.StartDate.ToString(),
            reservation.EndDate.ToString(),
            change.OldStatDate.ToString(),
            change.OldEndDate.ToString()));
        }
    }
}
