using System;
namespace FlightTracker.Metier.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public int Origin { get; set; } //Provenance
        public int Destination { get; set; } //Destination
        public int Plane { get; set; }
    }
}
