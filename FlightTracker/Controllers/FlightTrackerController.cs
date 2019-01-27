using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightTracker.Metier.Entities;
using FlightTracker.Service;
using FlightTracker.Metier.Miscs.DTO;
using FlightTracker.Models;
using FlightTracker.Metier.Miscs;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightTracker.Controllers
{
    public class FlightTrackerController : Controller
    {

        private readonly AirportService airportService;
        private readonly PlaneService planeService;
        private readonly FlightService flightService;

        public FlightTrackerController(DataContext context)
        {
            airportService = new AirportService(context);
            planeService = new PlaneService(context);
            flightService = new FlightService(context);
        }

        #region Flight
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
           List<ListFlightViewModel> models = new List<ListFlightViewModel>();
            var flights = await flightService.FlightsList();

           foreach (Flight flight in flights) {

                ListFlightViewModel model = new ListFlightViewModel() {
                    Destination = await airportService.AirportDetails(flight.Destination),
                    Origin = await airportService.AirportDetails(flight.Origin),
                    Plane = await planeService.PlaneDetails(flight.Plane),
                    Id = flight.Id
                };
                models.Add(model);
            }

            IEnumerable<ListFlightViewModel> liste = models;

            return View(liste);
        }

        // GET: /<controller>/
        public async Task<IActionResult> EditFlight(int Id)
        {
            var flight = await flightService.FlightDetails(Id);

            if (flight == null)
                return NotFound();

            ViewBag.AirportList = await airportService.AirportsList();
            ViewBag.PlaneList = await planeService.PlanesList();
            return View(flight);

        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFlight(int id, [Bind("Id,Origin,Destination,Plane")]  Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                ResultDTO resultDTO = await flightService.SaveFlight(flight);

                ViewData["Message"] = resultDTO.Msg;

                if (resultDTO.IsValid)
                    return RedirectToAction(nameof(Index));

            }
            return View(flight);

        }

        // GET: /<controller>/
        public async Task<IActionResult> AddFlight()
        {
            ViewBag.AirportList = await airportService.AirportsList();
            ViewBag.PlaneList =  await planeService.PlanesList();
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlight([Bind("Id,Origin,Destination,Plane")]  Flight flight)
        {
            if (ModelState.IsValid)
            {

                ResultDTO resultDTO = await flightService.SaveFlight(flight);

                ViewData["Message"] = resultDTO.Msg;

                if (resultDTO.IsValid)
                    return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: /<controller>/
        public async Task<IActionResult> DetailsFlight(int Id)
        {
            var flight = await flightService.FlightDetails(Id);

            if (flight == null)
                return NotFound();

            Airport Destination = await airportService.AirportDetails(flight.Destination);
            Airport Origin = await airportService.AirportDetails(flight.Origin);
            Plane Plane = await planeService.PlaneDetails(flight.Plane);
            FlightDetailsViewModel model = new FlightDetailsViewModel()
            {
                Destination = Destination,
                Origin = Origin,
                Plane = Plane,
                Id = flight.Id,
                Distance = CalculateDistance.DistanceInKmBetweenEarthCoordinates(Origin.Latitude, Origin.Longitude, Destination.Latitude, Destination.Longitude),
                FuelQuantity = CalculateDistance.FuelQuantityNecessary(Plane.FuelConsumption, Plane.TakeoffTime, Plane.TakeoffEffort)
            };

            return View(model);
        }

        #endregion

        #region Plane

        // GET: /<controller>/
        public async Task<IActionResult> PlaneList()
        {
            return View(await planeService.PlanesList());
        }

        // GET: /<controller>/
        public async Task<IActionResult> EditPlane(int Id)
        {
            var plane = await planeService.PlaneDetails(Id);
            if (plane == null)
                return NotFound();

            return View(plane);
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlane(int id, [Bind("Id, Name,TakeoffTime,TakeoffEffort,FuelConsumption")] Plane plane)
        {
            if (id != plane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ResultDTO resultDTO = await planeService.SavePlane(plane);

                ViewData["Message"] = resultDTO.Msg;

                if (resultDTO.IsValid)
                    return RedirectToAction(nameof(PlaneList));

            }
            return View(plane);
        }

        // GET: /<controller>/
        public IActionResult AddPlane()
        {
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlane([Bind("Id,Name,TakeoffTime,TakeoffEffort,FuelConsumption")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                ResultDTO resultDTO = await planeService.SavePlane(plane);

                ViewData["Message"] = resultDTO.Msg;

                if (resultDTO.IsValid)
                    return RedirectToAction(nameof(PlaneList));

            }
            return View(plane);
        }

        // GET: /<controller>/
        public async Task<IActionResult> DeletePlane(int Id)
        {
            var plane = await planeService.PlaneDetails(Id);
            if (plane == null)
                return NotFound();

            return View(plane);
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlane(string Id)
        {
            ResultDTO resultDTO = await planeService.DeletePlane(Convert.ToInt32(Id));

            ViewData["Message"] = resultDTO.Msg;

            if (resultDTO.IsValid)
                return RedirectToAction(nameof(PlaneList));

            return View();
        }

        #endregion

        #region Airport

        // GET: /<controller>/
        public async Task<IActionResult> AirportList()
        {
            return View(await airportService.AirportsList());
        }

        // GET: /<controller>/EditAirport/5
        public async Task<IActionResult> EditAirport(int Id)
        {
            Console.Out.WriteLine("ID DE L'AEROPORT " + Id);

            var airport = await airportService.AirportDetails(Id);
            if (airport == null)
                return NotFound();

            return View(airport);
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAirport(int id, [Bind("Id,Name,Longitude,Latitude")] Airport airport)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ResultDTO resultDTO = await airportService.SaveAirport(airport);

                ViewData["Message"] = resultDTO.Msg;

                if (resultDTO.IsValid)
                    return RedirectToAction(nameof(AirportList));

            }
            return View(airport);
        }

        // GET: /<controller>/
        public IActionResult AddAirport()
        {
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAirport([Bind("Id,Name,Longitude,Latitude")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                ResultDTO resultDTO = await airportService.SaveAirport(airport);

                ViewData["Message"] = resultDTO.Msg;

                if (resultDTO.IsValid)
                    return RedirectToAction(nameof(AirportList));

            }
            return View(airport);
        }

        // GET: /<controller>/
        public async Task<IActionResult> DeleteAirport(int Id)
        {
            var airport = await airportService.AirportDetails(Id);
            if (airport == null)
                return NotFound();

            return View(airport);
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAirport(string Id)
        {
            ResultDTO resultDTO = await airportService.DeleteAirport(Convert.ToInt32(Id));

            ViewData["Message"] = resultDTO.Msg;

            if (resultDTO.IsValid)
                return RedirectToAction(nameof(AirportList));

            return View();
        }
        #endregion
    }
}