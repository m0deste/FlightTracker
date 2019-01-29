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
    public class PlaneService : IPlaneRepository
    {
        private readonly DataContext _context;
        //POUR UN TEST Y'A CERTAINS DETAILS QUE J'AI HOMI COMME LA GESTION DES EXCEPTIONS

        public PlaneService(DataContext context)
        {
            this._context = context;
        }

        public async Task<Plane> PlaneDetails(int planeId)
        {
            if (planeId <= 0 )
                return null;
            return await _context.Plane.SingleOrDefaultAsync(a => a.Id == planeId);
        }

        public async Task<IEnumerable<Plane>> PlanesList()
        {
            return await _context.Plane.ToListAsync();
        }

        public async Task<ResultDTO> DeletePlane(int planeId)
        {
            ResultDTO result = new ResultDTO();
            result.IsValid = false;
            result.Msg = "Echec de la suppression!";


            var plane = await _context.Plane.SingleOrDefaultAsync(a => a.Id == planeId);
            _context.Plane.Remove(plane);
            int id = await _context.SaveChangesAsync();
            if (id > 0)
            {

                result.IsValid = true;
                result.Msg = "Suppresion éffectuée avec succès!";
            }


            return result;
        }

        public async Task<ResultDTO> SavePlane(Plane plane)
        {
            ResultDTO result = new ResultDTO();
            result.IsValid = false;
            result.Msg = "Erreur!";

            int id = 0;
            if (plane.Id <= 0)
            {
                //SAUVEGARDE
                _context.Add(plane);
                id = await _context.SaveChangesAsync();
            }
            else
            {

                //MODIFICATION

                try
                {
                    _context.Update(plane);
                    id = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!PlaneExists(plane.Id))
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

        private bool PlaneExists(int id)
        {
            return  _context.Plane.AnyAsync(e => e.Id == id).Result;
        }
    }
}
