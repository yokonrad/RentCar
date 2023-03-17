using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Cars.GetAllAsync();

            return View(data);
        }

        public async Task<IActionResult> MakeReservation(int id)
        {
            var cars = await _unitOfWork.Cars.GetAllAsync();
            var locations = await _unitOfWork.Locations.GetAllAsync();

            ViewData["Cars"] = new SelectList(cars, "Id", "Name", id);
            ViewData["LocationsFrom"] = new SelectList(locations, "Id", "Name");
            ViewData["LocationsTo"] = new SelectList(locations, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeReservation()
        {
            var cars = await _unitOfWork.Cars.GetAllAsync();
            var locations = await _unitOfWork.Locations.GetAllAsync();

            ViewData["Cars"] = new SelectList(cars, "Id", "Name", Request.Form["car.Id"]);
            ViewData["LocationsFrom"] = new SelectList(locations, "Id", "Name");
            ViewData["LocationsTo"] = new SelectList(locations, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeReservationConfirm([Bind("Id,Email,CarId,LocationFromId,LocationToId,DateFrom,DateTo")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var cars = await _unitOfWork.Cars.GetAllAsync();
                var locations = await _unitOfWork.Locations.GetAllAsync();

                var car = await _unitOfWork.Cars.GetByIdAsync(reservation.CarId);

                if (car is not null)
                {
                    var datesDiff = reservation.DateTo.Subtract(reservation.DateFrom).TotalDays;
                    var price = datesDiff * car.Price;

                    ViewData["Email"] = Request.Form["Email"];
                    ViewData["Cars"] = new SelectList(cars, "Id", "Name", reservation.CarId);
                    ViewData["LocationsFrom"] = new SelectList(locations, "Id", "Name", reservation.LocationFromId);
                    ViewData["LocationsTo"] = new SelectList(locations, "Id", "Name", reservation.LocationToId);
                    ViewData["DateFrom"] = Request.Form["DateFrom"];
                    ViewData["DateTo"] = Request.Form["DateTo"];
                    ViewData["Price"] = price;

                    return View(reservation);
                }
            }

            return RedirectToAction(nameof(MakeReservation), new { id = reservation.CarId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeReservationConfirmed([Bind("Id,Email,CarId,LocationFromId,LocationToId,DateFrom,DateTo")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork.Reservations.AddAsync(reservation) > 0)
                {
                    return View();
                }
            }

            return RedirectToAction(nameof(MakeReservation), new { id = reservation.CarId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}