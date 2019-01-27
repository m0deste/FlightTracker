using System;

namespace FlightTracker.Metier.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Longitude { get; set; } 
        public Double Latitude { get; set; }
    }
}
