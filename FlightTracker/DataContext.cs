using System;
using Microsoft.EntityFrameworkCore;
using FlightTracker.Metier.Entities;

namespace FlightTracker
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Airport> Airport { get; set; }
        public DbSet<Plane> Plane { get; set; }
        public DbSet<Flight> Flight { get; set; }
    }
}
