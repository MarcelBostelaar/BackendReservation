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
        //private DbContextOptionsBuilder _optionsBuilder;
        public ReserveerDBContext(DbContextOptions<ReserveerDBContext> options) : base(options)
        {

        }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantChange> ParticipantChanges { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationChange> ReservationChanges { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<TemperatureSensor> TemperatureSensors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPasswordLogin> UserPasswordLogins { get; set; }
        public DbSet<Whiteboard> Whiteboards { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>().HasKey(x => new { x.UserID, x.ReservationID });
            modelBuilder.Entity<Participant>().HasOne(x => x.User).WithMany(x => x.Participants).HasForeignKey(x => x.UserID);
            modelBuilder.Entity<Participant>().HasOne(x => x.Reservation).WithMany(x => x.Participants).HasForeignKey(x => x.ReservationID);

            modelBuilder.Entity<ParticipantChange>().HasKey(x => new { x.UserID, x.ReservationID, x.ChangeDate });
            modelBuilder.Entity<ParticipantChange>().HasOne(x => x.User).WithMany(x => x.ParticipantChanges).HasForeignKey(x => x.UserID);
            modelBuilder.Entity<ParticipantChange>().HasOne(x => x.Reservation).WithMany(x => x.ParticipantChanges).HasForeignKey(x => x.ReservationID);

            //modelBuilder.Entity<Participant>(
            //    x =>
            //    {
            //        x.HasKey(z => new { z.Reservation, z.User });
            //        x.HasOne<User>(z => z.User).WithMany();
            //        x.HasOne<Reservation>(z => z.Reservation).WithMany().HasForeignKey(z => new { z.Reservation, z.User });
            //    }
            //    );
            //modelBuilder.Entity<Participant>(
            //    x =>
            //    {
            //        x.HasOne<User>().WithMany();
            //        x.HasOne<Reservation>().WithMany();
            //        x.HasKey(t => new { t.Reservation, t.User });
            //    }
            //    );
            //modelBuilder.Entity<Participant>().HasOne(x => x.User).WithMany(x => x.Participants).HasForeignKey(x => new { u1 = x.User.Id, u2 = x.Reservation.Id });
            //modelBuilder.Entity<Participant>().hafo(x => new { u1 = x.Reservation.Id, u2 = x.User.Id });
            //modelBuilder.Entity<Participant>().HasOne(nameof(Reservation)).WithMany(nameof(Participant));
            //modelBuilder.Entity<Participant>().HasOne(nameof(User)).WithMany(nameof(Participant));
        }
    }
}
