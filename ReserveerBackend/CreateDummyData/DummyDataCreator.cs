using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend.CreateDummyData
{
    public class DummyDataCreator
    {
        public static void CreateDummyData(ReserveerDBContext db)
        {
            var RNG = new Random();
            for (int i = 0; i < 10; i++)
            {
                var newroom = new Room {
                    Name = "Dummy" + RNG.Next().ToString(),
                    Location = "Location" + RNG.Next().ToString(),
                    Capacity = RNG.Next(0, int.MaxValue),
                    TV = RNG.Next() % 2 == 0,
                    Smartboard = RNG.Next() % 2 == 0,
                    Powersupply = RNG.Next(0, 100)
                };
                db.Rooms.Add(newroom);
            }
            db.SaveChanges();


            //List<Item> items = new List<Item>()
            //{
            //    new Item{ Id=1, ItemName="Smartboard", Functional=true },
            //    new Item{ Id=2, ItemName="Whiteboard", Functional=true },
            //    new Item{ Id=3, ItemName="Television", Functional=true }
            //};
            //var room = new Room { Capacity = 30, Name = "H4.301" };
            //db.Rooms.Add(room);


            //List<Item> items2 = new List<Item>()
            //{
            //    new Item{ Id=4, ItemName="Smartboard", Functional=false },
            //    new Item{ Id=5, ItemName="Whiteboard", Functional=true }
            //};
            //var room2 = new Room { Capacity = 30, Items = items2, Temperature = 22, TemperatureDateTime = DateTime.Now, Name = "H4.302" };
            //db.Rooms.Add(room2);


            //List<Item> items3 = new List<Item>()
            //{
            //    new Item{ Id=6, ItemName="Smartboard", Functional=true },
            //    new Item{ Id=7, ItemName="Whiteboard", Functional=true },
            //    new Item{ Id=8, ItemName="Television", Functional=false }
            //};
            //var room3 = new Room { Capacity = 30, Items = items2, Temperature = 22, TemperatureDateTime = DateTime.Now, Name = "H4.303" };
            //db.Rooms.Add(room3);


            //var user = new User { Id = 1, Email = "0898976@hr.nl", EmailNotification = false, Role = "student" };
            //db.Users.Add(user);

            //db.SaveChanges();
        }
    }
}