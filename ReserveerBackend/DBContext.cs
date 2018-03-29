using Microsoft.EntityFrameworkCore;
using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveerBackend
{
    public class DBContext : DbContext
    {
        private DbContextOptionsBuilder _optionsBuilder;
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Item> Items { get; set; }

        /*public DbSet<UserSession> UserSession { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<DeliveryAddress> deliveryAddress { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<DeelProduct> DeelProduct { get; set; }

    public DbSet<ProductTaak> ProductTaak { get; set; }
    public DbSet<TaakCommit> TaakCommit { get; set; }
    public DbSet<Taak> Taak { get; set; }
    public DbSet<Material> Material { get; set; }
    public DbSet<Specificatie> Specificatie { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Room>().ToTable("Room");

            modelBuilder.Entity<Reservation>().ToTable("Reservation");

            modelBuilder.Entity<Item>().ToTable("Item");

            /*modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserSession>().ToTable("UserSession");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<DeliveryAddress>().ToTable("DeliveryAddress")
                .HasKey(i => i.KlantID);
            modelBuilder.Entity<Product>().ToTable("Product");

            modelBuilder.Entity<DeelProduct>().ToTable("DeelProduct");

            modelBuilder.Entity<Taak>().ToTable("Taak");
            modelBuilder.Entity<ProductTaak>().ToTable("ProductTaak")
                .HasKey(i => new { i.ProductID, i.TaakID });
            modelBuilder.Entity<Material>().ToTable("Material");
            modelBuilder.Entity<Specificatie>().ToTable("Specificatie");
            modelBuilder.Entity<DeelProduct>()
                .HasOne(e => e.Product);*/
        }
    }
}
