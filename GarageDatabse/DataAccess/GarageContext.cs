using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GarageDatabse.Models;

namespace GarageDatabse.DataAccess
{
    public class GarageContext : DbContext
    {
        public GarageContext() : base("DefaultConnection") { }

        public DbSet<ParkingPrice> ParkingPrices { get; set; }
        public DbSet<VehicleType> VehiclesTypes { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vehicle>().MapToStoredProcedures();

        }
    }
}