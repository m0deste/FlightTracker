using System;
using System.Collections.Generic;

namespace FlightTracker.Metier.Entities
{
    public class Plane
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int TakeoffTime { get; set; } //Temps de decollage
        public int TakeoffEffort { get; set; } //Effort de decollage
        public int FuelConsumption { get; set; } //Consommation de carburant par distance
       
    }
}
