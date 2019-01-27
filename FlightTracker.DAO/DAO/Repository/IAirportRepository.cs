using System;
using FlightTracker.Metier.Entities;
using FlightTracker.Metier.Miscs.DTO;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace FlightTracker.Metier.DAO.Repository
{
    public interface IAirportRepository
    {
        Task<ResultDTO> SaveAirport(Airport airport);
        Task<ResultDTO> DeleteAirport(int airportId);
        Task<IEnumerable<Airport>> AirportsList();
        Task<Airport> AirportDetails(int airportId);
    }
}
