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
    public class AirportService : IAirportRepository
    {
        private readonly DataContext _context;

        public AirportService(DataContext context) {
            this._context = context;
        }

        public async Task<Airport> AirportDetails(int airportId)
        {
            if (airportId <= 0)
                return null;

            return await _context.Airport.SingleOrDefaultAsync(a => a.Id == airportId);
        }

        public async Task<IEnumerable<Airport>> AirportsList()
        {
            return await _context.Airport.ToListAsync();
        }

        public async Task<ResultDTO> DeleteAirport(int airportId)
        {
            ResultDTO result = new ResultDTO();
            result.IsValid = false;
            result.Msg = "Erreur!";


            var airport = await _context.Airport.SingleOrDefaultAsync(a => a.Id == airportId);
            _context.Airport.Remove(airport);
            int id  = await _context.SaveChangesAsync();
            if (id > 0) {

                result.IsValid = true;
                result.Msg = "Suppresion éffectuée avec succès!";
            }


            return result;
        }

        public async Task<ResultDTO> SaveAirport(Airport airport)
        {
            ResultDTO result = new ResultDTO();
            result.IsValid = false;
            result.Msg = "Erreur!";

            int id = 0;
            if (airport.Id <= 0)
            {
                //SAUVEGARDE
                _context.Add(airport);
                id = await _context.SaveChangesAsync();
            }
            else {
                //MODIFICATION
                try
                {
                    _context.Update(airport);
                    id = await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!AirportExists(airport.Id))
                    {
                        result.IsValid = false;
                        result.Msg = "Aéroport introuvable!";
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

        private bool AirportExists(int id)
        {
            return _context.Airport.AnyAsync(e => e.Id == id).Result;
        }
    }
}
