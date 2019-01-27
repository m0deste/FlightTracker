using System;
using System.Collections.Generic;
using FlightTracker.Metier.Entities;

namespace FlightTracker.Models
{
    public class ListFlightViewModel
    {
        public int Id { get; set; }
        public Airport Origin { get; set; } //Provenance
        public Airport Destination { get; set; } //Destination
        public Plane Plane { get; set; }

    }
}
