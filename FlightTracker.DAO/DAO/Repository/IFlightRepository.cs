using System;
using FlightTracker.Metier.Entities;
using FlightTracker.Metier.Miscs.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightTracker.Metier.DAO.Repository
{
    public interface IFlightRepository
    {
        Task<ResultDTO> SaveFlight(Flight flight);
        Task<ResultDTO> DeleteFlight(int flightId);
        Task<IEnumerable<Flight>> FlightsList();
        Task<Flight> FlightDetails(int flightId);
    }
}
