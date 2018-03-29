using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.CreateDummyData
{
    public class DummyDataCreator
    {
        public static void CreateDummyData(DBContext db)
        {
            var items = db.Items.Where(x => x.Id < 10).ToList();
            var room = new Room { Capacity = 10, Items = items, Temperature = 20, TemperatureDateTime = DateTime.Now, Name = "room1"};
            db.Rooms.Add(room);
            db.SaveChanges();
        }
    }
}
