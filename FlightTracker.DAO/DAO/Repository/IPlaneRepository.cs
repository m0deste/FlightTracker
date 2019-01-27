using System;
using FlightTracker.Metier.Entities;
using FlightTracker.Metier.Miscs.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightTracker.Metier.DAO.Repository
{
    public interface IPlaneRepository
    {

        Task<ResultDTO> SavePlane(Plane plane);
        Task<ResultDTO> DeletePlane(int planeId);
        Task<IEnumerable<Plane>> PlanesList();
        Task<Plane> PlaneDetails(int planeId);
    }
}
