using Microsoft.EntityFrameworkCore;
using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend
{
    public class ReserveerDBContext : DbContext
    {
        private DbContextOptionsBuilder _optionsBuilder;
        public ReserveerDBContext(DbContextOptions<ReserveerDBContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Temperature>().HasKey(x => new { x.DateTime, x.Sensor });

            //modelBuilder.Entity<User>().ToTable("User");

            //modelBuilder.Entity<Room>().ToTable("Room");

            //modelBuilder.Entity<Reservation>().ToTable("Reservation");
        }
    }
}
