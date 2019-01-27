using System;
using System.Collections.Generic;
using FlightTracker.Metier.DAO.Repository;
using FlightTracker.Metier.Entities;
using FlightTracker.Metier.Miscs.DTO;
using FlightTracker;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightTracker.Service
{
    public class FlightService : IFlightRepository
    {

        private readonly DataContext _context;

        public FlightService(DataContext context)
        {
            this._context = context;
        }

        public async Task<Flight> FlightDetails(int flightId)
        {
            if (flightId <= 0)
                return null;
            return await _context.Flight.SingleOrDefaultAsync(a => a.Id == flightId);
        }

        public async Task<IEnumerable<Flight>> FlightsList()
        {
            return await _context.Flight.ToListAsync();
        }

        public async Task<ResultDTO> DeleteFlight(int flightId)
        {
            ResultDTO result = new ResultDTO();
            result.IsValid = false;
            result.Msg = "Erreur!";


            var flight = await _context.Flight.SingleOrDefaultAsync(a => a.Id == flightId);
            _context.Flight.Remove(flight);
            int id = await _context.SaveChangesAsync();
            if (id > 0)
            {

                result.IsValid = true;
                result.Msg = "Suppresion éffectuée avec succès!";
            }


            return result;
        }

        public async Task<ResultDTO> SaveFlight(Flight flight)
        {
            ResultDTO result = new ResultDTO();
            result.IsValid = false;
            result.Msg = "Erreur!";

            int id = 0;
            if (flight.Id == 0 || string.IsNullOrEmpty(flight.Id.ToString()))
            {
                //SAUVEGARDE
                _context.Add(flight);
                id = await _context.SaveChangesAsync();
           
             }
            else
            {
                //MODIFICATION
                try
                {
                    _context.Update(flight);
                    id = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!FlightExists(flight.Id))
                    {
                        result.IsValid = false;
                        result.Msg = "Vol introuvable!";
                    }
                    else
                    {
                        result.IsValid = false;
                        result.Msg = e.Message;
                       
                    }
                }
             }

            if (id > 0)
            {
                result.IsValid = true;
                result.Msg = "Sauvegarde éffectuée avec succès!";
            }

            return result;
        }

        private bool FlightExists(int id)
        {
            return _context.Flight.AnyAsync(e => e.Id == id).Result;
        }
    }
}
