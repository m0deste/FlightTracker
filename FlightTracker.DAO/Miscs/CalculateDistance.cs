using System;
namespace FlightTracker.Metier.Miscs
{
    public class CalculateDistance
    {

        public static Double DeegreesToRadians(Double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static Double FuelQuantityNecessary(int fuelConsumption, int takeoffTime, int takeoffEffort)
        {
            return fuelConsumption / (takeoffTime * takeoffEffort);
        }


        public static Double DistanceInKmBetweenEarthCoordinates(Double lat1, Double lon1, Double lat2, Double lon2)
        {
            var earthRadiusKm = 6371;

            var dLat = DeegreesToRadians(lat2 - lat1);
            var dLon = DeegreesToRadians(lon2 - lon1);

            lat1 =  DeegreesToRadians(lat1);
            lat2 = DeegreesToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }
    }
}
