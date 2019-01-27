using System;
namespace FlightTracker.Models
{
    public class FlightDetailsViewModel : ListFlightViewModel
    {
        public Double Distance { get; set; }
        public Double FuelQuantity { get; set; }
    }
}
