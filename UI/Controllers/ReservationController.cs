using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.Reservations.GetAllAsync();

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await _unitOfWork.Reservations.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var cars = from x in await _unitOfWork.Cars.GetAllAsync()
                       select new
                       {
                           x.Id,
                           Name = x.Id + ", " + x.Name,
                       };

            var locations = from x in await _unitOfWork.Locations.GetAllAsync()
                            select new
                            {
                                x.Id,
                                Name = x.Id + ", " + x.Name,
                            };

            ViewData["Cars"] = new SelectList(cars, "Id", "Name");
            ViewData["LocationsFrom"] = new SelectList(locations, "Id", "Name");
            ViewData["LocationsTo"] = new SelectList(locations, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,CarId,LocationFromId,LocationToId,DateFrom,DateTo")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Reservations.AddAsync(reservation);

                return RedirectToAction(nameof(Index));
            }

            return View(reservation);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _unitOfWork.Reservations.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            var cars = from x in await _unitOfWork.Cars.GetAllAsync()
                       select new
                       {
                           x.Id,
                           Name = x.Id + ", " + x.Name,
                       };

            var locations = from x in await _unitOfWork.Locations.GetAllAsync()
                            select new
                            {
                                x.Id,
                                Name = x.Id + ", " + x.Name,
                            };

            ViewData["Cars"] = new SelectList(cars, "Id", "Name", data.CarId);
            ViewData["LocationsFrom"] = new SelectList(locations, "Id", "Name", data.LocationFromId);
            ViewData["LocationsTo"] = new SelectList(locations, "Id", "Name", data.LocationToId);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,CarId,LocationFromId,LocationToId,DateFrom,DateTo")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Reservations.UpdateAsync(reservation);

                return RedirectToAction(nameof(Index));
            }

            return View(reservation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Reservations.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _unitOfWork.Reservations.GetByIdAsync(id);

            if (data != null)
            {
                _ = await _unitOfWork.Reservations.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}